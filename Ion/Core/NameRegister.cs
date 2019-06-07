using System.Collections.Generic;
using Ion.CodeGeneration.Helpers;

namespace Ion.Core
{
    public static class NameRegister
    {
        private static Dictionary<string, int> register = new Dictionary<string, int>();

        public static string Get(string name)
        {
            // Create the counter buffer.
            int counter = -1;

            // Retrieve the counter if it already exists.
            if (NameRegister.register.ContainsKey(name))
            {
                // Retrieve the counter.
                counter = NameRegister.register[name];
            }

            // Increment the counter.
            counter++;

            // Save the counter.
            NameRegister.register[name] = counter;

            // Form the string.
            string result = $".{name}.{counter}";

            // Return the resulting string.
            return result;
        }

        public static string GetBlock()
        {
            return NameRegister.Get(SpecialName.Block);
        }

        public static string GetString()
        {
            return NameRegister.Get("str");
        }

        public static string GetAnonymous()
        {
            return NameRegister.Get(SpecialName.Anonymous);
        }

        public static string GetLambda()
        {
            return NameRegister.Get(SpecialName.Lambda);
        }

        /// <summary>
        /// Reset all the name counters back to
        /// their initial values.
        /// </summary>
        public static void Reset()
        {
            NameRegister.register.Clear();
        }
    }
}
