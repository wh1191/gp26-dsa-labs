using System;

namespace DSA.Lab02
{
    public sealed class ArrayStack<T>
    {
        private readonly T[] _items;
        private int _count = 0;

        public ArrayStack(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            _items = new T[capacity];
        }

        /**
         * Q: If Count == 3, which array index contains the top item?
         * A: 2 (Count - 1)
         */
        public int Count => _count;
        public int Capacity => _items.Length;

        public void Push(T item) {
            // TODO: place the item at the logical top and update the state.
            if (_count >= _items.Length) {
                throw new InvalidOperationException("The stack is full.");
            }

            _items[_count++] = item;
        }

        public T Peek() {
            // TODO: return the logical top without removing it.
            if(_count <= 0) {
                throw new InvalidOperationException("The stack is empty.");
            }

            return _items[_count - 1];
        }

        public T Pop() {
            // TODO: remove and return the logical top.
            if (_count <= 0) {
                throw new InvalidOperationException("The stack is empty.");
            }
            //_count--;
            //T returnValue = _items[_count];
            T returnValue = _items[--_count];
            //T returnValue = _items[_count - 1];
            _items[_count] = default!; // Clear the reference for the Garbage Collector
            return returnValue;
        }

        public void Clear() {
            // Using _count is optimal for ArrayStack 
            Array.Clear(_items, 0, _count);
            /*for (int i = _count - 1; i >= 0; i--){
                _items[i] = default!;
            }*/
            _count = 0;
        }
    }
}
