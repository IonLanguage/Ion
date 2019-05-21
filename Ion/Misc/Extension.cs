using System;
using System.Collections.Generic;
using System.Linq;
using Ion.SyntaxAnalysis;
using LLVMSharp;
using TokenTypeMap = System.Collections.Generic.Dictionary<string, Ion.SyntaxAnalysis.TokenType>;

namespace Ion.Misc
{
    public static class Extension
    {
        public const string CutDelimiter = "...";

        public static string Capitalize(this string input)
        {
            switch (input)
            {
                case null:
                    {
                        throw new ArgumentNullException(nameof(input));
                    }

                case "":
                    {
                        throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                    }

                default:
                    {
                        return input.First().ToString().ToUpper() + input.Substring(1);
                    }
            }
        }

        /// <summary>
        /// Create a new LLVM block builder and position
        /// it accordingly.
        /// </summary>
        public static LLVMBuilderRef CreateBuilder(this LLVMBasicBlockRef block, bool positionAtStart = true)
        {
            // Create a new builder and link it to the global context.
            LLVMBuilderRef builder = LLVM.CreateBuilderInContext(LLVM.GetGlobalContext());

            // Position the builder at the beginning of the block.
            if (positionAtStart)
            {
                LLVM.PositionBuilderBefore(builder, block.GetLastInstruction());
            }
            // Otherwise, at the end of the block.
            else
            {
                LLVM.PositionBuilderAtEnd(builder, block);
            }

            // Return the linked builder.
            return builder;
        }

        public static TokenTypeMap SortByKeyLength(this TokenTypeMap map)
        {
            string[] keys = new string[map.Count];

            map.Keys.CopyTo(keys, 0);

            List<string> keyList = new List<string>(keys);

            // Sort the keys by length.
            keyList.Sort((a, b) =>
            {
                if (a.Length > b.Length) return -1;

                if (b.Length > a.Length) return 1;

                return 0;
            });

            Dictionary<string, TokenType> updated = new Dictionary<string, TokenType>();

            foreach (var item in keyList)
            {
                updated[item] = map[item];
            }

            return updated;
        }

        // TODO: Beginning no-cut (input is at the beginning) is not implemented, may need a starting index arg.
        /// <summary>
        /// Cut an input string to a maximum length and apply
        /// provided delimiters at the beginning and the end
        /// if applicable.
        /// </summary>
        public static string Cut(this string input, int maxLength, string delimiter = Extension.CutDelimiter)
        {
            // Copy the input string.
            string result = input;

            // Trim the result.
            result = result.Trim();

            // Determine if the result's length is larger than the max length.
            if (result.Length > maxLength)
            {
                // Cut and apply delimiter at the start. Remove an extra character for a space.
                result = result.Remove(0, delimiter.Length + 1);

                // Insert delimiter at the beginning of the result, along with a space.
                result = result.Insert(0, delimiter + " ");

                // Determine whether created string contains ending tokens.
                bool atEnd = result.Length == input.Length;

                // Determine if max length is met, otherwise proceed to cut the end.
                if (atEnd && result.Length > maxLength)
                {
                    // Compute the starting point of the remove range.
                    int removeStart = result.Length - delimiter.Length + 1;

                    // Compute the amount of characters to remove.
                    int removeCount = delimiter.Length + 1;

                    // Cut the end of the resulting string.
                    result = result.Remove(removeStart, removeCount);

                    // Insert delimiter, along with a space at the end.
                    result += " " + delimiter;
                }
            }

            // Return the resulting string.
            return result;
        }
    }
}
