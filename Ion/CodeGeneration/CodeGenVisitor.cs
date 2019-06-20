using System;
using System.Collections.Generic;
using Ion.CognitiveServices;
using Ion.Engine.Llvm;
using Ion.IR.Constructs;
using Ion.Tracking;
using Ion.Tracking.Symbols;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public interface ICodeGenVisitable
    {
        Construct Accept(CodeGenVisitor visitor);
    }

    public class CodeGenVisitor
    {
        protected readonly IonSymbolTable symbolTable;

        protected Stack<LlvmValue> valueStack;

        protected Stack<LlvmType> typeStack;

        protected LlvmBuilder builder;

        protected LlvmModule module;

        public CodeGenVisitor(LlvmModule module)
        {
            this.module = module;
            this.symbolTable = new IonSymbolTable();
            this.valueStack = new Stack<LlvmValue>();
            this.typeStack = new Stack<LlvmType>();
        }

        public Construct Visit(Global node)
        {
            // Create the global variable.
            LlvmValue global = LLVM.AddGlobal(context.Target.Target, node.Type.Emit(), node.Identifier);

            // Set the linkage to common.
            global.SetLinkage(LLVMLinkage.LLVMCommonLinkage);

            // Assign value if applicable.
            if (node.Value != null)
            {
                global.SetInitializer(this.Value.Emit());
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
            LlvmType[] args = node.Prototype.Args.Emit(context);

            // Emit the return type.
            LlvmType returnType = node.Prototype.ReturnType.Emit();

            // Emit the function type.
            LlvmType type = LLVM.FunctionType(returnType, args, node.Prototype.Args.Continuous);

            // Emit the external definition to context and capture the LLVM value reference.
            LlvmValue external = LLVM.AddFunction(context.Target.Target, node.Prototype.Identifier, type);

            // Determine if should be registered on the symbol table.
            if (!this.symbolTable.functions.Contains(node.Prototype.Identifier))
            {
                // Register the external definition as a function in the symbol table.
                this.symbolTable.functions.Add(new FunctionSymbol(node.Prototype.Identifier, external, this.Prototype.Args.Continuous));
            }
            // Otherwise, issue a warning.
            else
            {
                // TODO
                System.Console.WriteLine($"Warning: Extern definition '{node.Prototype.Identifier}' being re-defined");
            }

            // Return the resulting LLVM value reference.
            return external;
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
            LlvmType[] args = node.Prototype.Args.Emit(context);

            // Emit the return type
            LlvmType returnType = node.Prototype.ReturnType.Emit();

            // Emit the function type.
            LlvmType type = LLVM.FunctionType(returnType, args, node.Prototype.Args.Continuous);

            // Create the function.
            LlvmFunction function = this.module.CreateFunction(node.Prototype.Identifier, type);

            // Create the argument index counter.
            uint argIndexCounter = 0;

            // Name arguments.
            foreach (FormalArg arg in this.Prototype.Args.Values)
            {
                // Name the argument.
                LLVM.SetValueName(LLVM.GetParam(function, argIndexCounter), arg.Identifier);

                // Increment the index counter for next iteration.
                argIndexCounter++;
            }

            // Emit the body to its corresponding context.
            LlvmBlock body = this.Body.Emit(functionContext);

            // TODO: Missing support for native attribute emission.
            // Emit attributes as first-class instructions if applicable.
            foreach (Attribute attribute in this.Attributes)
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
            this.symbolTable.functions.Add(new FunctionSymbol(node.Prototype.Identifier, function, node.Prototype.Args.Continuous));

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
            foreach (Expr expr in this.Expressions)
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
                this.builder.CreateReturn(this.ReturnExpr.Emit(builderContext));
            }

            // Append the resulting block onto the stack.
            this.valueStack.Push(block);

            // Return the node.
            return node;
        }

        public Construct Visit(PrimitiveType node)
        {
            // Invoke LLVM type resolver, will automatically handle possible non-existent error.
            this.typeStack.Push(Resolvers.LlvmTypeFromName(node.TokenValue));

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
                // Delegate to primitive type construct.
                type = new PrimitiveType(node.Token.Value).Emit();
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
