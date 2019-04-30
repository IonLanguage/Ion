using System.Collections.Generic;
using System.Text;
using Ion.Core;
using Ion.Misc;
using Ion.SyntaxAnalysis;

namespace Ion.ErrorReporting
{
    public class TraceableError : Error
    {
        public Stack<ErrorTraceStackItem> Stack { get; } = new Stack<ErrorTraceStackItem>();

        protected readonly TokenStream stream;

        public TraceableError(TokenStream stream, string message, string name = InternalErrorNames.Generic) : base(message, name)
        {
            this.stream = stream;
        }

        public override string ToString()
        {
            StringBuilder traceBuilder = new StringBuilder();

            traceBuilder.AppendLine(base.ToString());

            foreach (var stackItem in this.Stack)
            {
                traceBuilder.AppendLine($"\t{stackItem.TraceString}");
            }

            return traceBuilder.ToString();
        }
    }
}
