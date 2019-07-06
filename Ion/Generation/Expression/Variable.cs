using Ion.Parsing;

namespace Ion.Generation
{
    public class Variable : Construct
    {
        public override ConstructType ConstructType => ConstructType.Variable;

        public string Identifier { get; }

        public PathResult Path { get; }

        public Variable(string identifier) : this(new PathResult(identifier))
        {
            this.Identifier = identifier;
        }

        public Variable(PathResult path)
        {
            this.Path = path;
            this.Identifier = this.Path.ToString();
        }
    }
}
