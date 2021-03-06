using Ion.CodeGeneration.Helpers;
using Ion.SyntaxAnalysis;

namespace Ion.AST
{
    public class EntityNode<TResult, TContext> : Node<IPipe<TResult, TContext>>
    {
        public TokenType Operator { get; set; }

        // TODO: Instead of .GetHashCode(), entities should be associated with an unique index.
        public EntityNode(IPipe<TResult, TContext> value) : base(value.GetHashCode(), value)
        {
            //
        }
    }
}
