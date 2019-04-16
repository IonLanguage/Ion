using System;
using LlvmSharpLang.CodeGen;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{

    public class StatementParser : Named, IParser<IStatement>
    {
        /// <summary>
        /// Parses multiple statement types such as variable
        /// declarations, assignments, function returns and
        /// other common block statements.
        /// </summary>
        public IStatement Parse(TokenStream stream)
        {
            Token nextToken = stream.Peek();

            // Return statement.
            if (nextToken.Type == TokenType.KeywordReturn)
            {
                // TODO: Implement.
                throw new NotImplementedException();

                // return new FunctionReturnParser().Parse(stream);
            }
            // Expression.
            else if (nextToken.Type == TokenType.Id)
            {
                return new ExprParser().Parse(stream);
            }

            // Otherwise, unexpected token.
            throw new Exception("Unable to identify statement type");
        }
    }

}
