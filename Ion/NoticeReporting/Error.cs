using Ion.Misc;

namespace Ion.NoticeReporting
{
    public class Error : Notice
    {
        public static string Create(string message, string name = InternalErrorNames.Generic)
        {
            // Capitalize the error name.
            string capitalizedName = name.Capitalize();

            // Forge and return the error string.
            return $"{capitalizedName}Error: {message}";
        }

        public static void Write(string message, string name = InternalErrorNames.Generic)
        {
            Error.Create(message, name);
        }

        public readonly string name;

        public Error(string message, string sourceFileName, string name = InternalErrorNames.Generic) : base(NoticeTitles.Error, message, sourceFileName)
        {
            this.name = name;
        }

        public void Write()
        {
            Error.Write(this.message, this.name);
        }

        public override string ToString()
        {
            return Error.Create(this.message, this.name);
        }
    }
}
