using System.Collections.Generic;
using System.Text;
using Ion.SyntaxAnalysis;

namespace Ion.ErrorReporting
{
    public class TraceableError : Error
    {
        protected readonly TokenStream stream;

        public TraceableError(TokenStream stream, string message, string name = InternalErrorNames.Generic) : base(
            message, name)
        {
            this.stream = stream;
        }

        public Stack<ErrorTraceStackItem> Stack { get; } = new Stack<ErrorTraceStackItem>();

        public override string ToString()
        {
            // Create the trace string builder.
            StringBuilder traceBuilder = new StringBuilder();

            traceBuilder.AppendLine(base.ToString());

            foreach (ErrorTraceStackItem stackItem in this.Stack)
            {
                traceBuilder.AppendLine($"\t{stackItem.TraceString}");
            }

            return traceBuilder.ToString();
        }
    }
}
