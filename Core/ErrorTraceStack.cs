namespace LlvmSharpLang
{
    public struct ErrorTraceStackItem
    {
        public string SourceFile { get; set; }

        public int Position { get; set; }

        public string TraceString
        {
            get => $"at {this.SourceFile} : {this.Position}";
        }
    }
}
