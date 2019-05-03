using Ion.CodeGeneration.Structure;
using Ion.SyntaxAnalysis;

namespace Ion.AST
{
    public class EntityNode<TResult, TContext> : Node<IEntity<TResult, TContext>>
    {
        // TODO: Instead of .GetHashCode(), entities should be associated with an unique index.
        public EntityNode(IEntity<TResult, TContext> value) : base(value.GetHashCode(), value)
        {
            //
        }

        public TokenType Operator { get; set; }
    }
}