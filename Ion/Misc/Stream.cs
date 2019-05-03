using System;
using System.Collections.Generic;
using System.Text;

namespace Ion.Misc
{
    // TODO: Upon adding (inserting) or removing items, the index will NOT update.
    public class Stream<T> : List<T>
    {
        protected int index;

        protected int pivotIndex = -1;

        public Stream()
        {
            // Prepare the initial enumerator.
            Reset();
        }

        public Stream(T[] items) : base(items)
        {
            // Prepare the initial enumerator.
            Reset();
        }

        public int Index => index;

        public bool LastItem => index == Count - 1;

        /// <summary>
        ///     Set the peek pivot relative to the current
        ///     index.
        /// </summary>
        public void SetRelativePivot(int amount)
        {
            SetPivot(index + amount);
        }

        /// <summary>
        ///     Set the peek pivot.
        /// </summary>
        public void SetPivot(int at)
        {
            if (at < 1) throw new ArgumentException("Unexpected index to be less than one");

            pivotIndex = at;
        }

        /// <summary>
        ///     Reset the peek pivot, no longer affecting
        ///     peek actions.
        /// </summary>
        public void ResetPivot()
        {
            pivotIndex = -1;
        }

        /// <summary>
        ///     Reset the index to zero.
        /// </summary>
        public void Reset()
        {
            index = 0;
        }

        public bool Skip()
        {
            // Ensure not overflowing.
            if (LastItem) return false;

            index++;

            return true;
        }

        public T Next()
        {
            Skip();

            return Get();
        }

        public T Peek(int amount = 1)
        {
            var newIndex = index;

            // Apply pivot if applicable.
            if (pivotIndex != -1) newIndex += pivotIndex;

            // Amount cannot be zero.
            if (amount == 0) throw new ArgumentException("Amount cannot be zero");

            // Return first or last item if index overflows.
            if (DoesIndexOverflow(newIndex + amount))
            {
                // Return program end token.
                if (amount > 0)
                    // TODO
                    return this[this.Count - 1];

                // Otherwise, return first item.
                return this[0];
            }

            return this[index + amount];
        }

        /// <summary>
        ///     Determine if the provided index
        ///     will overflow the amount of items
        ///     currently available.
        /// </summary>
        public bool DoesIndexOverflow(int at)
        {
            return at < 0 || Count - 1 < at;
        }

        public T Get()
        {
            return this[index];
        }

        public override string ToString()
        {
            // Create the string builder.
            var result = new StringBuilder();

            // Loop through all values.
            foreach (T token in this)
                // Append the value's string representation to the result.
                result.AppendLine(token.ToString());

            // Build the final string.
            return result.ToString();
        }
    }
}