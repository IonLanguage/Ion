using System.Collections.Generic;

namespace LlvmSharpLang.Misc
{
    public class Stream<T> : List<T>
    {
        public int Index => this.index;

        protected int index;

        protected IEnumerator<T> enumerator;

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

        public void Reset()
        {
            this.index = 0;
            this.enumerator = this.GetEnumerator();
        }

        public bool Skip()
        {
            bool successful = this.enumerator.MoveNext();

            // Ensure not overflowing.
            if (successful && this.index + 1 <= this.Count - 1)
            {
                this.index++;
            }

            return successful;
        }

        public T Next()
        {
            this.Skip();

            return this.enumerator.Current;
        }

        public T Peek()
        {
            return this[this.index];
        }

        public T Get()
        {
            return this.enumerator.Current;
        }
    }
}
