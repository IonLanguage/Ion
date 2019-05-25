using System.Collections.Generic;
using Ion.Misc;

namespace Ion.Tracking
{
    public abstract class SymbolTable<T> where T : INamedSymbol
    {
        public readonly Dictionary<string, T> symbols = new Dictionary<string, T>();

        public T this[string key]
        {
            get
            {
                return this.symbols[key];
            }
        }

        public bool Contains(string identifier)
        {
            return this.symbols.ContainsKey(identifier);
        }

        public void Add(T item)
        {
            this.symbols.Add(item.Identifier, item);
        }
    }
}
