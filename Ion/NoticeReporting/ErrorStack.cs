using System.Collections.Generic;
using System.Text;
using Ion.SyntaxAnalysis;

namespace Ion.NoticeReporting
{
    public class ErrorStack
    {
        protected readonly TokenStream stream;

        protected readonly List<ErrorStackItem> items;

        public ErrorStack(TokenStream stream)
        {
            this.stream = stream;
            this.items = new List<ErrorStackItem>();
        }

        public void Append(Error error)
        {
            // Create and populate the item.
            ErrorStackItem item = new ErrorStackItem
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

            foreach (ErrorStackItem stackItem in this.items)
            {
                traceBuilder.AppendLine($"\t{stackItem.TraceString}");
            }

            return traceBuilder.ToString();
        }
    }
}
