using System;
using System.Collections.Generic;
using Ion.CodeGeneration;
using Ion.Core;
using Ion.SyntaxAnalysis;
using LLVMSharp;

namespace Ion.Parsing
{
    public class CallExprParser : IParser<CallExpr>
    {
        protected readonly string identifier;

        public CallExprParser(string identifier)
        {
            this.identifier = identifier;
        }

        public CallExprParser(PathResult path) : this(path.ToString())
        {
            //
        }

        public CallExpr Parse(ParserContext context)
        {
            // Invoke the function call argument parser.
            List<Expr> args = new CallArgsParser().Parse(context);

            // Create the function call expression entity.
            CallExpr functionCall = new CallExpr(this.identifier, args);

            // Return the function call expression.
            return functionCall;
        }
    }
}
