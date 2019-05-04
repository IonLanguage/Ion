namespace Ion.ErrorReporting
{
    public struct ErrorTraceStackItem
    {
        public string SourceFile { get; set; }

        public int Position { get; set; }

        public string TraceString => $"at {this.SourceFile} : {this.Position}";
    }
}