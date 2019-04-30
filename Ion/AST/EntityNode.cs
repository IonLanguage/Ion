using Ion.SyntaxAnalysis;
using Ion.CodeGeneration.Structure;

namespace Ion.AST
{
    public class EntityNode<TResult, TContext> : Node<IEntity<TResult, TContext>>
    {
        public TokenType Operator { get; set; }

        // TODO: Instead of .GetHashCode(), entities should be associated with an unique index.
        public EntityNode(IEntity<TResult, TContext> value) : base(value.GetHashCode(), value)
        {
            //
        }
    }
}
