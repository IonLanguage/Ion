namespace Ion.Core
{
    public static class NameRegister
    {
        private static int str;

        private static int anonymous;

        static NameRegister()
        {
            // Reset to initial values.
            NameRegister.ResetAll();
        }

        public static string GetString()
        {
            // Capture the result.
            string result = $"str_{NameRegister.str}";

            // Increment the counter.
            NameRegister.str++;

            // Return the result.
            return result;
        }

        public static string GetAnonymous()
        {
            // Capture the result.
            string result = $"anonymous_{NameRegister.anonymous}";

            // Increment the counter.
            NameRegister.anonymous++;

            // Return the result.
            return result;
        }

        /// <summary>
        /// Reset all the name counters back to
        /// their initial values.
        /// </summary>
        public static void ResetAll()
        {
            NameRegister.str = 0;
            NameRegister.anonymous = 0;
        }
    }
}
