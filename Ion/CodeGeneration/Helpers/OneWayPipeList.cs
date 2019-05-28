using System.Collections.Generic;

namespace Ion.CodeGeneration.Helpers
{
    public class OneWayPipeList<T> : List<T> where T : IOneWayPipe<T>
    {
        public T[] Emit()
        {
            // Create the result buffer list.
            List<T> result = new List<T>();

            // Emit all the items onto the buffer list.
            foreach (T item in this)
            {
                result.Add(item.Emit());
            }

            // Return the resulting array.
            return result.ToArray();
        }
    }
}
