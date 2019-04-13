using System.Collections.Generic;
using System.Text;
using LlvmSharpLang.Core;
using LlvmSharpLang.Misc;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang
{
    public class TraceableError : Error
    {
        public List<ErrorTraceStackItem> Stack { get; } = new List<ErrorTraceStackItem>();

        protected readonly TokenStream stream;

        public TraceableError(TokenStream stream, string message, string name = Error.defaultName) : base(message, name)
        {
            this.stream = stream;
        }

        public override string Get()
        {
            StringBuilder traceBuilder = new StringBuilder();

            traceBuilder.AppendLine(base.Get());

            foreach (var stackItem in this.Stack)
            {
                traceBuilder.AppendLine($"\t{stackItem.TraceString}");
            }

            return traceBuilder.ToString();
        }
    }
}
