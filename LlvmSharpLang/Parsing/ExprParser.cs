using System;
using LLVMSharp;
using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.CognitiveServices;
using LlvmSharpLang.Misc;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class ExprParser : IParser<Expr>
    {
        public Expr Parse(TokenStream stream)
        {
            // Parse the left side of the expression.
            Expr leftSide = new PrimaryExprParser().Parse(stream);

            // Ensure left side was successfully parsed.
            if (leftSide == null)
            {
                throw new Exception("Unable to parse left-side of the expression");
            }

            // Invoke the binary expression parser.
            Expr expr = new BinaryOpRightSideParser(leftSide, 0).Parse(stream);

            // Return the parsed expression.
            return expr;
        }
    }
}
