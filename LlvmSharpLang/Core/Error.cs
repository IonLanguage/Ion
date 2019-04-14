using System;
using LlvmSharpLang.SyntaxAnalysis;
using LlvmSharpLang.Misc;

namespace LlvmSharpLang.Core
{
    public class Error
    {
        public const string defaultName = "Generic";

        public static string Create(string message, string name = Error.defaultName)
        {
            string capitalizedName = name.Capitalize();

            return $"{capitalizedName}Error: {message}";
        }

        public static void Write(string message, string name = Error.defaultName)
        {
            Error.Create(message, name);
        }

        public readonly string message;

        public readonly string name;

        public Error(string message, string name = Error.defaultName)
        {
            this.message = message;
            this.name = name;
        }

        public void Write()
        {
            Error.Write(this.message, this.name);
        }

        public virtual string Get()
        {
            return Error.Create(this.message, this.name);
        }
    }
}
