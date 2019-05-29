using System;
using System.Collections.Generic;
using System.Text;

namespace Ion.Misc
{
    // TODO: Upon adding (inserting) or removing items, the index will NOT update.
    public class Stream<T> : List<T>
    {
        public T Current => this.Get();

        public int Index => this.index;

        /// <summary>
        /// Whether the index currently points to the last item.
        /// </summary>
        public bool IsLastItem => this.index == this.Count - 1;

        protected int index;

        protected int pivotIndex = -1;

        public Stream()
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
        /// Set the peek pivot relative to the current
        /// index.
        /// </summary>
        public void SetRelativePivot(int amount)
        {
            this.SetPivot(this.index + amount);
        }

        /// <summary>
        /// Set the peek pivot.
        /// </summary>
        public void SetPivot(int at)
        {
            // Index must be 1 or larger.
            if (at < 1)
            {
                throw new ArgumentException("Unexpected index to be less than one");
            }

            // Set the pivot.
            this.pivotIndex = at;
        }

        /// <summary>
        /// Reset the peek pivot, no longer affecting
        /// peek actions.
        /// </summary>
        public void ResetPivot()
        {
            this.pivotIndex = -1;
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
            // Ensure not overflowing.
            if (this.IsLastItem)
            {
                return false;
            }

            this.index++;

            return true;
        }

        public T Next()
        {
            // Skip current item.
            this.Skip();

            // Return the new item.
            return this.Get();
        }

        public T Peek(int amount = 1)
        {
            // Create the new index buffer, default to the current index.
            int newIndex = this.index;

            // Apply pivot if applicable.
            if (this.pivotIndex != -1)
            {
                newIndex += this.pivotIndex;
            }

            // Amount cannot be zero.
            if (amount == 0)
            {
                throw new ArgumentException("Amount cannot be zero");
            }

            // Return first or last item if index overflows.
            if (this.DoesIndexOverflow(newIndex + amount))
            {
                // Return program end token.
                if (amount > 0)
                {
                    // TODO
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
        public bool DoesIndexOverflow(int at)
        {
            return at < 0 || this.Count - 1 < at;
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
            foreach (T token in this)
            {
                // Append the value's string representation to the result.
                result.AppendLine(token.ToString());
            }

            // Build the final string.
            return result.ToString();
        }
    }
}
