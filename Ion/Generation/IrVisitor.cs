using System;
using System.Collections.Generic;
using Ion.CognitiveServices;
using Ion.Engine.Llvm;
using Ion.IR.Constructs;
using Ion.Misc;
using Ion.Syntax;
using Ion.Tracking;
using LLVMSharp;
using static Ion.Syntax.Constants;

namespace Ion.Generation
{
    public interface IrVisitable
    {
        Construct Accept(IrVisitor visitor);
    }

    public class IrVisitor
    {
        protected readonly IonSymbolTable symbolTable;

        protected Stack<LlvmBlock> blockStack;

        protected Stack<LlvmValue> valueStack;

        protected Stack<LlvmType> typeStack;

        protected LlvmBuilder builder;

        protected LlvmModule module;

        public IrVisitor(LlvmModule module, LlvmBuilder builder)
        {
            this.module = module;
            this.builder = builder;
            this.symbolTable = new IonSymbolTable();
            this.valueStack = new Stack<LlvmValue>();
            this.typeStack = new Stack<LlvmType>();
            this.blockStack = new Stack<LlvmBlock>();
        }

        public Construct Visit(Construct node)
        {
            return node.Accept(this);
        }

        public Construct Visit(BinaryExpr node)
        {
            // Ensure operation is registered.
            if (!Constants.operatorBuilderMap.ContainsKey(node.Operation))
            {
                throw new Exception($"Unexpected unsupported operation: {node.Operation}");
            }

            // Create the operation invoker.
            SimpleMathBuilderInvoker invoker = Constants.operatorBuilderMap[node.Operation];

            // Visit the left and right sides.
            this.Visit(node.LeftSide);
            this.Visit(node.RightSide);

            // Pop off respective values.
            LlvmValue rightSideValue = this.valueStack.Pop();
            LlvmValue leftSideValue = this.valueStack.Pop();

            // TODO: Side expressions emitting to context?
            // Invoke the operation generator and wrap the resulting value.
            LlvmValue value = invoker(this.builder.Unwrap(), leftSideValue.Unwrap(), rightSideValue.Unwrap(), node.Identifier).Wrap();

            // Append the value onto the stack.
            this.valueStack.Push(value);

            // Return the node.
            return node;
        }

        public Construct Visit(Call node)
        {
            // TODO: Implement.
            throw new NotImplementedException();
        }

        public Construct Visit(Boolean node)
        {
            // Create a new primitive boolean type instance.
            PrimitiveType type = PrimitiveTypeFactory.Boolean();

            // Resolve the value.
            LlvmValue valueRef = Resolver.Literal(node.tokenType, node.value, type).Wrap();

            // Append the value onto the stack.
            this.valueStack.Push(valueRef);

            // Return the node.
            return node;
        }

        public Construct Visit(Pipe node)
        {
            // TODO: Callee is hard-coded as a string.
            // Create the function call expression.
            Call functionCall = new Call(node.TargetName, node.Arguments);

            // Visit the function call.
            this.Visit(functionCall);

            // Pop the resulting value off the stack.
            LlvmValue value = this.valueStack.Pop();

            // Return the node.
            return node;
        }

        public Construct Visit(Global node)
        {
            // Visit the type.
            this.Visit(node.Type);

            // Pop the type off the stack.
            LlvmType type = this.typeStack.Pop();

            // Create the global variable.
            LlvmGlobal global = this.module.CreateGlobal(node.Identifier, type);

            // Set the linkage to common.
            global.SetLinkage(LLVMLinkage.LLVMCommonLinkage);

            // Assign initial value if applicable.
            if (node.InitialValue != null)
            {
                // Visit the initial value.
                this.Visit(node.InitialValue);

                // Pop off the initial value off the stack.
                LlvmValue initialValue = this.valueStack.Pop();

                // Set the initial value.
                global.SetInitialValue(initialValue);
            }

            // Append the global onto the stack.
            this.valueStack.Push(global);

            // Return the node.
            return node;
        }

        public Construct Visit(Directive node)
        {
            // Register the directive on the symbol table.
            this.symbolTable.directives.Add(node.Key, node.Value);

            // Return the node.
            return node;
        }

        public Construct Visit(Extern node)
        {
            // Ensure prototype is set.
            if (node.Prototype == null)
            {
                throw new Exception("Unexpected external definition's prototype to be null");
            }

            // Emit the formal arguments.
            LlvmType[] args = node.Prototype.Arguments.Emit(context);

            // Visit the prototype's return type.
            this.Visit(node.Prototype.ReturnType);

            // Pop the return type off the stack.
            LlvmType returnType = this.typeStack.Pop();

            // Emit the function type.
            LlvmType type = LlvmTypeFactory.Function(returnType, args, node.Prototype.Arguments.Continuous);

            // Emit the external definition to context and capture the LLVM value reference.
            LlvmValue @extern = this.module.CreateFunction(node.Prototype.Identifier, type);

            // Determine if should be registered on the symbol table.
            if (!this.symbolTable.functions.Contains(node.Prototype.Identifier))
            {
                // Register the external definition as a function in the symbol table.
                this.symbolTable.functions.Add((LlvmFunction)@extern);
            }
            // Otherwise, issue a warning.
            else
            {
                // TODO
                System.Console.WriteLine($"Warning: Extern definition '{node.Prototype.Identifier}' being re-defined");
            }

            // Push the resulting value onto the stack.
            this.valueStack.Push(@extern);

            // Return the resulting LLVM value reference.
            return node;
        }

        public Construct Visit(Function node)
        {
            // TODO: Implement.
            throw new NotImplementedException();
        }

        public Construct Visit(Block node)
        {
            // Create the block's statement list.
            List<LlvmValue> statements = new List<LlvmValue>();

            // Visit and process the statements.
            foreach (Construct statement in node.Statements)
            {
                // Visit the statement.
                this.Visit(statement);

                // Pop off the value off the stack and append to the buffer list.
                statements.Add(this.valueStack.Pop());
            }

            // No value was returned.
            if (!node.HasReturnExpr)
            {
                this.builder.CreateReturnVoid();
            }
            // Otherwise, process the set return value.
            else
            {
                // Visit the return construct.
                this.Visit(node.ReturnConstruct);

                // Pop the return construct off the stack.
                LlvmValue returnConstruct = this.valueStack.Pop();

                // Create the return construct.
                this.builder.CreateReturn(returnConstruct);
            }

            // Create the block.
            Section block = new Section(node.Identifier);

            // Append the resulting block onto the stack.
            this.valueStack.Push(block);

            // Return the node.
            return node;
        }

        public Construct Visit(PrimitiveType node)
        {
            // Invoke LLVM type resolver, will automatically handle possible non-existent error.
            this.typeStack.Push(Resolver.LlvmTypeFromName(node.TokenValue));

            // Return the node.
            return node;
        }

        public Construct Visit(Type node)
        {
            // Create the result buffer.
            LlvmType type;

            // Use LLVM type resolver if token is a primitive type.
            if (TokenIdentifier.IsPrimitiveType(node.Token))
            {
                // Create and visit the type.
                this.Visit(new PrimitiveType(node.Token.Value));

                // Pop the type off the stack.
                type = this.typeStack.Pop();
            }
            // Otherwise, look it up on the structs dictionary, on the symbol table.
            else if (this.symbolTable.structs.Contains(node.Token.Value))
            {
                type = this.symbolTable.structs[node.Token.Value].Value;
            }
            // At this point, provided token is not a valid type.
            else
            {
                throw new Exception($"Token is not a primitive nor user-defined type: '{node.Token.Value}'");
            }

            // Convert result to an array if applicable.
            if (node.IsArray)
            {
                type.ConvertToArray(node.ArrayLength.Value);
            }

            // Convert result to a pointer if applicable.
            if (node.IsPointer)
            {
                type.ConvertToPointer();
            }

            // Append the resulting type onto the stack.
            this.typeStack.Push(type);

            // Return the node.
            return node;
        }
    }
}
