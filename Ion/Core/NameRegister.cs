using System.Collections.Generic;
using Ion.CodeGeneration.Helpers;

namespace Ion.Core
{
    public static class GlobalNameRegister
    {
        private static Dictionary<string, int> register = new Dictionary<string, int>();

        public static string Get(string name)
        {
            // Create the counter buffer.
            int counter = -1;

            // Retrieve the counter if it already exists.
            if (GlobalNameRegister.register.ContainsKey(name))
            {
                // Retrieve the counter.
                counter = GlobalNameRegister.register[name];
            }

            // Increment the counter.
            counter++;

            // Save the counter.
            GlobalNameRegister.register[name] = counter;

            // Form the string.
            string result = $".{name}.{counter}";

            // Return the resulting string.
            return result;
        }

        public static string GetBlock()
        {
            return GlobalNameRegister.Get(SpecialName.Block);
        }

        public static string GetString()
        {
            return GlobalNameRegister.Get("str");
        }

        public static string GetAnonymous()
        {
            return GlobalNameRegister.Get(SpecialName.Anonymous);
        }

        public static string GetLambda()
        {
            return GlobalNameRegister.Get(SpecialName.Lambda);
        }

        /// <summary>
        /// Reset all the name counters back to
        /// their initial values.
        /// </summary>
        public static void Reset()
        {
            GlobalNameRegister.register.Clear();
        }
    }
}
