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
    public interface ICodeGenVisitable
    {
        Construct Accept(CodeGenVisitor visitor);
    }

    public class CodeGenVisitor
    {
        protected readonly IonSymbolTable symbolTable;

        protected Stack<LlvmBlock> blockStack;

        protected Stack<LlvmValue> valueStack;

        protected Stack<LlvmType> typeStack;

        protected LlvmBuilder builder;

        protected LlvmModule module;

        public CodeGenVisitor(LlvmModule module, LlvmBuilder builder)
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
            // Create an argument buffer list.
            List<LlvmValue> arguments = new List<LlvmValue>();

            // Emit the call arguments.
            foreach (Construct argument in node.Arguments)
            {
                // Continue if the argument is null.
                if (argument == null)
                {
                    continue;
                }

                // Visit the argument.
                this.Visit(argument);

                // Pop the argument off the value stack.
                LlvmValue argumentValue = this.valueStack.Pop();

                // Append argument value to the argument buffer list.
                arguments.Add(argumentValue);
            }

            // Ensure the function has been emitted.
            if (!this.symbolTable.functions.Contains(node.TargetIdentifier))
            {
                throw new Exception($"Call to a non-existent function named '{node.TargetIdentifier}' performed");
            }

            // Retrieve the target function.
            LlvmFunction target = this.symbolTable.functions[node.TargetIdentifier];

            // Ensure argument count is correct (with continuous arguments).
            if (target.ContinuousArgs && arguments.Count < target.ArgumentCount - 1)
            {
                throw new Exception($"Target function requires at least {target.ArgumentCount - 1} argument(s)");
            }
            // Otherwise, expect the argument count to be exact.
            else if (arguments.Count != target.ArgumentCount)
            {
                throw new Exception($"Argument amount mismatch, target function requires exactly {target.ArgumentCount} argument(s)");
            }

            // Create the function call.
            Instruction functionCall = new Instruction(this.Identifier, this.TargetIdentifier);

            // Append the value onto the stack.
            this.valueStack.Push(functionCall);

            // Return the node.
            return node;
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
            // Ensure body was provided or created.
            if (node.Body == null)
            {
                throw new Exception("Unexpected function body to be null");
            }
            // Ensure prototype is set.
            else if (node.Prototype == null)
            {
                throw new Exception("Unexpected function prototype to be null");
            }
            // Ensure that body returns a value if applicable.
            else if (!node.Prototype.ReturnType.IsVoid && !node.Body.HasReturnExpr)
            {
                throw new Exception("Functions that do not return void must return a value");
            }

            // Emit the argument types.
            LlvmType[] arguments = node.Prototype.Arguments.Emit(context);

            // Visit the return type node.
            this.Visit(node.Prototype.ReturnType);

            // Pop off the return type off the stack.
            LlvmType returnType = this.typeStack.Pop();

            // Emit the function type.
            LlvmType type = LlvmTypeFactory.Function(returnType, arguments, node.Prototype.Arguments.Continuous);

            // Create the function.
            LlvmFunction function = this.module.CreateFunction(node.Prototype.Identifier, type);

            // Create the argument index counter.
            uint argumentIndexCounter = 0;

            // Name arguments.
            foreach (FormalArg formalArgument in node.Prototype.Arguments.Values)
            {
                // Retrieve the argument.
                LlvmValue argument = function.GetArgumentAt(argumentIndexCounter);

                // Name the argument.
                argument.SetName(formalArgument.Identifier);

                // Increment the index counter for next iteration.
                argumentIndexCounter++;
            }

            // Visit the body.
            this.Visit(node.Body);

            // Pop the body off the stack.
            LlvmBlock body = this.blockStack.Pop();

            // Position the body's builder at the beginning.
            body.Builder.PositionAtStart();

            // TODO: Missing support for native attribute emission.
            // Emit attributes as first-class instructions if applicable.
            foreach (Attribute attribute in node.Attributes)
            {
                // Emit the attribute onto the body's builder context.
                attribute.Emit(bodyContext);
            }

            // Ensures the function does not already exist
            if (this.symbolTable.functions.Contains(node.Prototype.Identifier))
            {
                throw new Exception($"A function with the identifier '{node.Prototype.Identifier}' already exists");
            }

            // Register the function on the symbol table.
            this.symbolTable.functions.Add(function);

            // Append the function onto the stack.
            this.valueStack.Push(function);

            // Return the node.
            return node;
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
