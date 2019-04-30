using System;
using Ion.SyntaxAnalysis;
using Ion.Misc;

namespace Ion.ErrorReporting
{
    public class Error
    {
        public static string Create(string message, string name = InternalErrorNames.Generic)
        {
            string capitalizedName = name.Capitalize();

            return $"{capitalizedName}Error: {message}";
        }

        public static void Write(string message, string name = InternalErrorNames.Generic)
        {
            Error.Create(message, name);
        }

        public readonly string message;

        public readonly string name;

        public Error(string message, string name = InternalErrorNames.Generic)
        {
            this.message = message;
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
