using System.Text;
using Ion.Misc;
using Ion.Syntax;

namespace Ion.NoticeReporting
{
    public class Trace
    {
        /// <summary>
        /// The amount of tokens that will be joined
        /// together into a string from the token stream.
        /// </summary>
        public const int JoinAmount = 5;

        /// <summary>
        /// The maximum amount of characters to keep
        /// when the joined token string is being
        /// cutted.
        /// </summary>
        public const int CutMaxLength = 20;

        protected readonly TokenStream stream;

        public Trace(TokenStream stream)
        {
            this.stream = stream;
        }

        public override string ToString()
        {
            // Join tokens.
            string result = this.stream.Join(this.stream.Index, Trace.JoinAmount);

            // Cut the result.
            result = result.Cut(Trace.CutMaxLength);

            // Create a string builder for the track arrow.
            StringBuilder trackArrowBuilder = new StringBuilder();

            // Position the track arrow at the beginning of the resulting string, after delimiter and a space.
            trackArrowBuilder.Append(' ', Extension.CutDelimiter.Length + 1);

            // Build the track arrow string.
            string trackArrow = trackArrowBuilder.ToString();

            // Append a newline and the track arrow to the result.
            result += "\n" + trackArrow;

            // Return the resulting string.
            return result;
        }
    }
}
