using System;

namespace Ion.NoticeReporting
{
    public class Warning : Notice
    {
        public Warning(string message, string sourceFileName) : base(NoticeTitles.Warning, message, sourceFileName)
        {
            //
        }

        public override string ToString()
        {
            // TODO: Implement.
            throw new NotImplementedException();
        }
    }
}
