namespace Ion.Core
{
    public static class NameCounter
    {
        private static int str;

        private static int anonymous;

        static NameCounter()
        {
            // Reset to initial values.
            NameCounter.ResetAll();
        }

        public static string GetString()
        {
            // Capture the result.
            string result = $"str_{NameCounter.str}";

            // Increment the counter.
            NameCounter.str++;

            // Return the result.
            return result;
        }

        public static string GetAnonymous()
        {
            // Capture the result.
            string result = $"anonymous_{NameCounter.anonymous}";

            // Increment the counter.
            NameCounter.anonymous++;

            // Return the result.
            return result;
        }

        /// <summary>
        /// Reset all the name counters back to
        /// their initial values.
        /// </summary>
        public static void ResetAll()
        {
            NameCounter.str = 0;
            NameCounter.anonymous = 0;
        }
    }
}
