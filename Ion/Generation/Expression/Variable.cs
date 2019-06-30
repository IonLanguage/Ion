using Ion.Parsing;

namespace Ion.Generation
{
    public class Variable : Construct
    {
        public override ConstructType ConstructType => ConstructType.Variable;

        public PathResult Path { get; }

        public Variable(string name) : this(new PathResult(name))
        {
            this.SetName(name);
        }

        public Variable(PathResult path)
        {
            this.Path = path;
            this.SetName(this.Path.ToString());
        }
    }
}
