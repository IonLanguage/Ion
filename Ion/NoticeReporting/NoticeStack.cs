using Ion.SyntaxAnalysis;

namespace Ion.NoticeReporting
{
    public class NoticeStack : ReadOnlyNoticeStack
    {
        public NoticeStack(TokenStream stream) : base(stream)
        {
            //
        }

        public void Append(Notice notice)
        {
            // Create and populate the item.
            NoticeStackItem item = new NoticeStackItem
            {
                Position = this.stream.Index,
                SourceFile = notice.sourceFileName
            };

            // Add the item onto the stack.
            this.items.Add(item);
        }

        public ReadOnlyNoticeStack AsReadOnly()
        {
            return (ReadOnlyNoticeStack)this;
        }
    }
}
