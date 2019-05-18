namespace Ion.NoticeReporting
{
    public struct NoticeStackItem
    {
        public string SourceFile { get; set; }

        public int Position { get; set; }

        public string TraceString => $"at {this.SourceFile} : {this.Position}";
    }
}
