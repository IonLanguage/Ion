#nullable enable

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

        protected Stack<IR.Constructs.Construct> stack;

        protected Stack<Kind> kindStack;

        protected LlvmBuilder builder;

        protected LlvmModule module;

        public IrVisitor(LlvmModule module, LlvmBuilder builder)
        {
            this.module = module;
            this.builder = builder;
            this.symbolTable = new IonSymbolTable();
            this.stack = new Stack<IR.Constructs.Construct>();
            this.kindStack = new Stack<Kind>();
        }

        public Construct Visit(Construct node)
        {
            return node.Accept(this);
        }

        public Construct VisitExtension(Construct node)
        {
            return node.VisitChildren(this);
        }

        public Construct VisitValue(Value node)
        {
            throw new NotImplementedException();
        }

        public Construct VisitBinaryExpr(BinaryExpr node)
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
            LlvmValue rightSideValue = this.stack.Pop();
            LlvmValue leftSideValue = this.stack.Pop();

            // TODO: Side expressions emitting to context?
            // Invoke the operation generator and wrap the resulting value.
            LlvmValue value = invoker(this.builder.Unwrap(), leftSideValue.Unwrap(), rightSideValue.Unwrap(), node.Identifier).Wrap();

            // Append the value onto the stack.
            this.stack.Push(value);

            // Return the node.
            return node;
        }

        public Construct VisitCall(Call node)
        {
            // TODO: Implement.
            throw new NotImplementedException();
        }

        public Construct VisitBoolean(Boolean node)
        {
            // Create a new primitive boolean type instance.
            PrimitiveType type = PrimitiveTypeFactory.Boolean();

            // Resolve the value.
            LlvmValue valueRef = Resolver.Literal(node.tokenType, node.value, type).Wrap();

            // Append the value onto the stack.
            this.stack.Push(valueRef);

            // Return the node.
            return node;
        }

        public Construct VisitPipe(Pipe node)
        {
            // TODO: Callee is hard-coded as a string.
            // Create the function call expression.
            Call functionCall = new Call(node.TargetName, node.Arguments);

            // Visit the function call.
            this.VisitCall(functionCall);

            // Pop the resulting value off the stack.
            LlvmValue value = this.stack.Pop();

            // Return the node.
            return node;
        }

        public Construct VisitGlobal(Global node)
        {
            // Visit the type.
            this.VisitType(node.Type);

            // Pop the kind off the stack.
            Kind kind = this.kindStack.Pop();

            // Create the value buffer.
            IR.Constructs.Value? value = null;

            if (node.InitialValue != null)
            {
                // Visit the initial value.
                this.VisitValue(node.InitialValue);

                // Pop the value off the stack.
                value = (IR.Constructs.Value)this.stack.Pop();
            }

            // Create the global variable.
            IR.Constructs.Global global = new IR.Constructs.Global(node.Identifier, kind, value);

            // Append the global onto the stack.
            this.stack.Push(global);

            // Return the node.
            return node;
        }

        public Construct VisitDirective(Directive node)
        {
            // Register the directive on the symbol table.
            this.symbolTable.directives.Add(node.Key, node.Value);

            // Return the node.
            return node;
        }

        public Construct VisitExtern(Extern node)
        {
            // Visit the prototype's return type.
            this.VisitType(node.Prototype.ReturnType);

            // Pop the return type off the stack.
            Kind returnKind = this.kindStack.Pop();

            // Create the arguments buffer list.
            List<(Kind, Reference)> arguments = new List<(Kind, Reference)>();

            // Visit the prototype's arguments.
            foreach (Type type in node.Prototype.Arguments)
            {
                // Visit type.
                this.VisitType(type);

                // Pop the resulting kind off the stack.
                Kind kind = this.kindStack.Pop();

                // TODO: Hard-coded argument references/names.
                // Append it onto the arguments list.
                arguments.Add((kind, new Reference("arg")));
            }

            // TODO: Infinite arguments support (currently hard-coded).
            IR.Constructs.Prototype prototype = new IR.Constructs.Prototype(node.Prototype.Identifier, arguments.ToArray(), returnKind, false);

            // Create the extern construct.
            IR.Constructs.Extern @extern = new IR.Constructs.Extern(prototype);

            // Append onto the stack.
            this.stack.Push(@extern);

            // Return the node.
            return node;
        }

        public Construct VisitFunction(Function node)
        {
            // TODO: Implement.
            throw new NotImplementedException();
        }

        public Construct VisitArray(Array node)
        {
            // Prepare the value buffer list.
            List<IR.Constructs.Value> values = new List<IR.Constructs.Value>();

            // Iterate and emit all the values onto the buffer list.
            foreach (Value value in node.Values)
            {
                // Visit the value.
                this.VisitValue(value);

                // Pop the value off the stack and append it onto the list.
                values.Add((IR.Constructs.Value)this.stack.Pop());
            }

            // Visit the element type.
            this.VisitType(node.Type);

            // Pop the resulting kind off the stack.
            Kind elementKind = this.kindStack.Pop();

            // Create the array IR construct.
            IR.Constructs.Array array = new IR.Constructs.Array(elementKind, values.ToArray());

            // Append the array onto the stack.
            this.stack.Push(array);

            // Return the node.
            return node;
        }

        public Construct VisitBlock(Block node)
        {
            // Create the block's statement list.
            List<IR.Constructs.Construct> statements = new List<IR.Constructs.Construct>();

            // Visit and process the statements.
            foreach (Construct statement in node.Statements)
            {
                // Visit the statement.
                this.Visit(statement);

                // Pop off the value off the stack and append to the buffer list.
                statements.Add(this.stack.Pop());
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
                LlvmValue returnConstruct = this.stack.Pop();

                // Create the return construct.
                this.builder.CreateReturn(returnConstruct);
            }

            // Create the block.
            Section block = new Section(node.Identifier);

            // Append the resulting block onto the stack.
            this.stack.Push(block);

            // Return the node.
            return node;
        }

        public Construct VisitPrimitiveType(PrimitiveType node)
        {
            // Invoke LLVM type resolver, will automatically handle possible non-existent error.
            this.typeStack.Push(Resolver.LlvmTypeFromName(node.TokenValue));

            // Return the node.
            return node;
        }

        public Construct VisitType(Type node)
        {
            // Create the result buffer.
            Kind kind;

            // Use LLVM type resolver if token is a primitive type.
            if (TokenIdentifier.IsPrimitiveType(node.Token))
            {
                // Create and visit the type.
                this.VisitPrimitiveType(new PrimitiveType(node.Token.Value));

                // Pop the type off the stack.
                kind = this.typeStack.Pop();
            }
            // Otherwise, look it up on the structs dictionary, on the symbol table.
            else if (this.symbolTable.structs.Contains(node.Token.Value))
            {
                kind = this.symbolTable.structs[node.Token.Value].Value;
            }
            // At this point, provided token is not a valid type.
            else
            {
                throw new Exception($"Token is not a primitive nor user-defined type: '{node.Token.Value}'");
            }

            // Convert result to an array if applicable.
            if (node.IsArray)
            {
                kind.ConvertToArray(node.ArrayLength.Value);
            }

            // Convert result to a pointer if applicable.
            if (node.IsPointer)
            {
                kind.ConvertToPointer();
            }

            // Append the resulting type onto the stack.
            this.typeStack.Push(kind);

            // Return the node.
            return node;
        }
    }
}
