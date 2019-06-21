using Ion.Parsing;

namespace Ion.Generation
{
    public class Namespace
    {
        public readonly PathResult path;

        public Namespace(PathResult path)
        {
            this.path = path;
        }

        public void Invoke(Module context)
        {
            // Retrieve the corresponding path name.
            string name = this.path.ToString();

            // Apply the name.
            context.Identifier = name;
        }
    }
}
