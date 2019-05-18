using System;
using System.Runtime.Serialization;

namespace Ion.NoticeReporting
{
    public class ParserException : Exception
    {
        public ParserException()
        {
            //
        }
        public ParserException(string message) : base(message)
        {
            //
        }

        public ParserException(string message, Exception inner) : base(message, inner)
        {
            //
        }

        protected ParserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            //
        }
    }
}
