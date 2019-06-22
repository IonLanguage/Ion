using System;
using System.Collections.Generic;
using Ion.CognitiveServices;
using Ion.Engine.Llvm;
using Ion.IR.Constructs;
using Ion.Tracking;
using LLVMSharp;

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

        public Construct Visit(FormalArgs node)
        {
            // Create the resulting argument list.
            List<LLVMTypeRef> arguments = new List<LLVMTypeRef>();

            // Emit all arguments.
            foreach (FormalArg argument in node.Values)
            {
                // Emit and append each argument.
                arguments.Add(argument.Emit(context));
            }

            // TODO
            // Return the resulting argument array.
            // return args.ToArray();

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
            // Create the block.
            Section block = new Section();

            // Create the block's statement list.
            List<Instruction> statements = new List<Instruction>();

            // Emit the expressions.
            foreach (Expr expr in node.Expressions)
            {
                context.Target.Emit(expr.Emit());
            }

            // No value was returned.
            if (!node.HasReturnExpr)
            {
                this.builder.CreateReturnVoid();
            }
            // Otherwise, emit the set return value.
            else
            {
                // Emit the return expression.
                this.builder.CreateReturn(node.ReturnExpr.Emit(builderContext));
            }

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
