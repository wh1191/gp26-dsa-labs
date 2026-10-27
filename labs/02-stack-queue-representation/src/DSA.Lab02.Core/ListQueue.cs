using System;
using System.Collections.Generic;

namespace DSA.Lab02
{
    // Correct behavior, deliberately straightforward representation.
    // Removing index 0 from List<T> shifts the remaining elements left.
    public sealed class ListQueue<T>
    {
        private readonly List<T> _items = new List<T>();

        public int Count => _items.Count;

        public void Enqueue(T item)
        {
            _items.Add(item);
        }

        public T Peek()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            return _items[0];
        }

        /**
         * Which element is removed?
         * The first element (index 0).
         * 
         * What must List<T> do with the remaining elements afterwards?
         * Probably move all the elements, so that the second element becomes the first and so on. This is so that the referencing 
         * becomes correct while also not continuosly use more memory (even when removing elements, applies to arrays at least).
         * 
         * If the queue contains 100,000 items, which operation may become expensive?
         * In theory, Dequeue, due to the element moving (or shifting). But C# uses a data structure called Circular/Ring Buffer, 
         * which just moves pointers (head and tail), so the operation runs in O(1) time.
         */
        public T Dequeue()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            T item = _items[0];
            _items.RemoveAt(0);
            return item;
        }
    }
}
