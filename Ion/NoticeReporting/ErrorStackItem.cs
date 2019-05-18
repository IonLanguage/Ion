namespace Ion.NoticeReporting
{
    public struct ErrorStackItem
    {
        public string SourceFile { get; set; }

        public int Position { get; set; }

        public string TraceString => $"at {this.SourceFile} : {this.Position}";
    }
}
