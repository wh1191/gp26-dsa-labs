using System;
using System.Collections.Generic;

namespace DSA.Lab04B
{
    /// <summary>
    /// A correct but deliberately simple baseline.
    /// Enqueue appends in O(1), while finding/removing the cheapest item is O(n).
    /// </summary>
    /// 

    /**
     * Which operation is cheap?
     * Enqueue, because it's O(1) amortized. Amortized means that it's usually constant (O(1)), but it can 
     * sometimes become something like O(n) depending on what's going on in the background. For example,
     * if the array becomes full, it needs to the following: 
     * - Allocate a new array (double the size) in memory.
     * - Copy all existing elements from the old array into the new one (O(n)).
     * - Throw away the old array.
     * - Add the new element.
     * 
     * The reason why it's O(1) amortized is because it simply appends the new PriorityEntry<T> to the 
     * end of the underlying List<T>, it doesn't need to do any sorting or searching. It is (in most 
     * cases) instantaneous regardless of how many elememts are already in the queue.
     * 
     * Which operation repeats work?
     * PeekNext and DequeueNext repeats work. Both funcitons rely on the FindMinimumIndex() function, 
     * which performs a linear scan through the entire list every single time it's called. 
     * It forgets everything about the ordering of elements and must do a new scan of all 'n' 
     * elements from the beginning.
     * 
     * What happens when 100 000 candidates are waiting?
     * Performance will lead to a O(n^2) time complexity. Dequeuing a list of 100 000 elements
     * leads to a first scan that scans all 100 000 elements, then each new scan will shrink with 1 
     * (the next one will scan 99 999 elements). Total operations scale quadratically, which will 
     * result in a very high number of CPU instruction checks. This will most likely cause high 
     * CPU usage with noticeable stuttering or just outright freezing.
     * 
     * Why did Problem D from the World Map workshop motivate a different representation?
     * 
     */
    public sealed class LinearDispatchQueue<T>
    {
        private readonly List<PriorityEntry<T>> _entries = new List<PriorityEntry<T>>();

        public int Count => _entries.Count;

        public void Enqueue(T item, int priority)
        {
            _entries.Add(new PriorityEntry<T>(item, priority));
        }

        public PriorityEntry<T> PeekNext()
        {
            return _entries[FindMinimumIndex()];
        }

        public PriorityEntry<T> DequeueNext()
        {
            int index = FindMinimumIndex();
            PriorityEntry<T> result = _entries[index];
            _entries.RemoveAt(index);
            return result;
        }

        private int FindMinimumIndex()
        {
            if (_entries.Count == 0)
            {
                throw new InvalidOperationException("The dispatch queue is empty.");
            }

            int bestIndex = 0;

            for (int i = 1; i < _entries.Count; ++i)
            {
                if (_entries[i].Priority < _entries[bestIndex].Priority)
                {
                    bestIndex = i;
                }
            }

            return bestIndex;
        }
    }
}
