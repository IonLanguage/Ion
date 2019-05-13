using Ion.CodeGeneration.Structure;
using Ion.Parsing;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class Import : IReaction<LLVMContextRef>
    {
        public PathResult Path { get; }

        public Import(PathResult path)
        {
            this.Path = path;
        }

        public void Invoke(LLVMContextRef context)
        {
            // TODO
        }
    }
}
