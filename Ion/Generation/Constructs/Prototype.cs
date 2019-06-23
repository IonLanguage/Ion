namespace Ion.Generation
{
    public class Prototype
    {
        public string Identifier { get; }

        public Type[] Arguments { get; set; }

        // TODO: Must verify return type to be a type emitter (either Type or PrimitiveType).
        public Construct ReturnType { get; }

        public Prototype(string identifier, Type[] arguments, Construct returnType)
        {
            this.Identifier = identifier;
            this.Arguments = arguments;
            this.ReturnType = returnType;
        }
    }
}
