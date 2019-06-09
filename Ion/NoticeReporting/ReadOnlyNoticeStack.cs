using System.Collections.Generic;
using System.Text;
using Ion.Syntax;

namespace Ion.NoticeReporting
{
    public class ReadOnlyNoticeStack
    {
        protected readonly TokenStream stream;

        protected readonly List<NoticeStackItem> items;

        public ReadOnlyNoticeStack(TokenStream stream)
        {
            this.stream = stream;
            this.items = new List<NoticeStackItem>();
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
