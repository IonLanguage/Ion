namespace Ion.CodeGeneration
{
    public class Prototype
    {
        public string Identifier { get; }

        public FormalArgs Args { get; set; }

        // TODO: Must verify return type to be a type emitter (either Type or PrimitiveType).
        public Construct ReturnType { get; }

        public Prototype(string identifier, FormalArgs args, Construct returnType)
        {
            this.Identifier = identifier;
            this.Args = args;
            this.ReturnType = returnType;
        }
    }
}
