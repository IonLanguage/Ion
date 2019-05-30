using Ion.CodeGeneration.Helpers;
using Ion.Parsing;

namespace Ion.CodeGeneration
{
    public class Directive : IGenericReaction
    {
        public string Key { get; }

        public string Value { get; }

        public Directive(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        public Directive(PathResult key, string value) : this(key.ToString(), value)
        {
            //
        }

        public void Invoke(IGenericPipeContext context)
        {
            // Register the directive on the symbol table.
            context.SymbolTable.directives.Add(this.Key, this.Value);
        }
    }
}
