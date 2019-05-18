using Ion.Misc;

namespace Ion.ErrorReporting
{
    public class Error
    {
        public readonly string message;

        public readonly string sourceFileName;

        public readonly string name;

        public Error(string message, string sourceFileName, string name = InternalErrorNames.Generic)
        {
            this.message = message;
            this.sourceFileName = sourceFileName;
            this.name = name;
        }

        public static string Create(string message, string name = InternalErrorNames.Generic)
        {
            // Capitalize the error name.
            string capitalizedName = name.Capitalize();

            // Forge and return the error string.
            return $"{capitalizedName}Error: {message}";
        }

        public static void Write(string message, string name = InternalErrorNames.Generic)
        {
            Create(message, name);
        }

        public void Write()
        {
            Write(this.message, this.name);
        }

        public override string ToString()
        {
            return Create(this.message, this.name);
        }
    }
}
