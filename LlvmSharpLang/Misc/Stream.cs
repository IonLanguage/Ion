using System.Collections.Generic;

namespace LlvmSharpLang.Misc
{
    // TODO: Upon adding or removing items, the index will NOT update.
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

        public T Peek()
        {
            if (this.LastItem)
            {
                return this.Get();
            }

            return this[this.index + 1];
        }

        public T Get()
        {
            return this[this.index];
        }
    }
}
