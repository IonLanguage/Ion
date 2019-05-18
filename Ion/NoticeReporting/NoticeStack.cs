using System.Collections.Generic;
using System.Text;
using Ion.SyntaxAnalysis;

namespace Ion.NoticeReporting
{
    public class NoticeStack
    {
        protected readonly TokenStream stream;

        protected readonly List<NoticeStackItem> items;

        public NoticeStack(TokenStream stream)
        {
            this.stream = stream;
            this.items = new List<NoticeStackItem>();
        }

        public void Append(Error error)
        {
            // Create and populate the item.
            NoticeStackItem item = new NoticeStackItem
            {
                Position = this.stream.Index,
                SourceFile = error.sourceFileName
            };

            // Add the item onto the stack.
            this.items.Add(item);
        }

        public override string ToString()
        {
            // Create the trace string builder.
            StringBuilder traceBuilder = new StringBuilder();

            traceBuilder.AppendLine(base.ToString());

            foreach (NoticeStackItem stackItem in this.items)
            {
                traceBuilder.AppendLine($"\t{stackItem.TraceString}");
            }

            return traceBuilder.ToString();
        }
    }
}
