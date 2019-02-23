using System.Collections;
using System.Collections.Generic;

namespace Nota.Data
{
    public readonly struct IndexAccessor<TKey, TValue> : IEnumerable<TValue>
    {
        private readonly IDictionary<TKey, TValue> dictionary;

        public IndexAccessor(IDictionary<TKey, TValue> dictionary)
        {
            this.dictionary = dictionary;
        }

        public TValue this[TKey index]
        {
            get
            {
                if (this.dictionary.ContainsKey(index))
                    return this.dictionary[index];
                return default;
            }
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            foreach (var item in this.dictionary.Values)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();


    }
}