using System;
using System.Collections.Generic;
using System.Text;

namespace Ion.Misc
{
    // TODO: Upon adding (inserting) or removing items, the index will NOT update.
    public class Stream<T> : List<T>
    {
        public int Index => this.index;

        public bool LastItem => this.index == this.Count - 1;

        protected int index;

        public Stream() : base()
        {
            // Prepare the initial enumerator.
            this.Reset();
        }

        public Stream(T[] items) : base(items)
        {
            // Prepare the initial enumerator.
            this.Reset();
        }

        /// <summary>
        /// Reset the index to zero.
        /// </summary>
        public void Reset()
        {
            this.index = 0;
        }

        public bool Skip()
        {
            bool successful = false;

            // Ensure not overflowing.
            if (!this.LastItem)
            {
                this.index++;
                successful = true;
            }

            return successful;
        }

        public T Next()
        {
            this.Skip();

            return this.Get();
        }

        public T Peek(int amount = 1)
        {
            // Amount cannot be zero.
            if (amount == 0)
            {
                throw new ArgumentException("Amount cannot be zero");
            }
            // Return first or last item if index overflows.
            else if (this.DoesIndexOverflow(this.index + amount))
            {
                // Return last item.
                if (amount > 0)
                {
                    return this[this.Count - 1];
                }

                // Otherwise, return first item.
                return this[0];
            }

            return this[this.index + amount];
        }

        /// <summary>
        /// Determine if the provided index
        /// will overflow the amount of items
        /// currently available.
        /// </summary>
        public bool DoesIndexOverflow(int index)
        {
            return index < 0 || this.Count - 1 < index;
        }

        public T Get()
        {
            return this[this.index];
        }

        public override string ToString()
        {
            // Create the string builder.
            StringBuilder result = new StringBuilder();

            // Loop through all values.
            foreach (var token in this)
            {
                // Append the value's string representation to the result.
                result.AppendLine(token.ToString());
            }

            // Build the final string.
            return result.ToString();
        }
    }
}
