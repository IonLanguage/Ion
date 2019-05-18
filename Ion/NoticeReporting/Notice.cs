namespace Ion.NoticeReporting
{
    public abstract class Notice
    {
        public readonly string title;

        public readonly string message;

        public readonly string sourceFileName;

        protected Notice(string title, string message, string sourceFileName)
        {
            this.title = title;
            this.message = message;
            this.sourceFileName = sourceFileName;
        }

        public abstract override string ToString();
    }
}
