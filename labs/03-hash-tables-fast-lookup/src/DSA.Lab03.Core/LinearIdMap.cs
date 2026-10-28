using System.Collections.Generic;

namespace DSA.Lab03
{
    /**
     * What is the worst case when the key is missing?
     * Since the key is missing, the lookup needs to go through every element by comparing the key, O(n).
     * The function may return false and 'value' most likely becomes null (depends what type it is).
     * 
     * What changes when the collection grows from 100 entries to 100,000?
     * The number of comparisons the function might have to go through increases linearly. 
     * 
     * What operation are we trying to improve?
     * Key lookup/search needs to be improved (including insertion that rely on searching for an existing key).
     * List needs to be changed to something like a Dictionary or a Hash Table, since those use a hash function
     * that jump directly to the memory location of a key. This improves the lookup time to O(1) on average.
     */

    public sealed class LinearIdMap<TValue>
    {
        private readonly List<Entry<TValue>> _entries = new List<Entry<TValue>>();

        public int Count => _entries.Count;

        public void Set(int key, TValue value)
        {
            for (int i = 0; i < _entries.Count; ++i)
            {
                if (_entries[i].Key == key)
                {
                    _entries[i] = new Entry<TValue>(key, value);
                    return;
                }
            }

            _entries.Add(new Entry<TValue>(key, value));
        }

        public bool TryGetValue(int key, out TValue value)
        {
            for (int i = 0; i < _entries.Count; ++i)
            {
                if (_entries[i].Key == key)
                {
                    value = _entries[i].Value;
                    return true;
                }
            }

            value = default!;
            return false;
        }
    }
}
