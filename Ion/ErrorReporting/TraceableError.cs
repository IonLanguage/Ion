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
            var traceBuilder = new StringBuilder();

            traceBuilder.AppendLine(base.ToString());

            foreach (ErrorTraceStackItem stackItem in Stack) traceBuilder.AppendLine($"\t{stackItem.TraceString}");

            return traceBuilder.ToString();
        }
    }
}