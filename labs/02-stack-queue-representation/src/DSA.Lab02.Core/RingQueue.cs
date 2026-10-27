using System;

namespace DSA.Lab02
{
    public sealed class RingQueue<T>
    {
        private readonly T[] _items;
        private int _head = -1;
        private int _tail = -1;
        private int _count = 0;

        public RingQueue(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            _items = new T[capacity];
        }

        public int Count => _count;
        public int Capacity => _items.Length;

        public void Enqueue(T item) {
            // TODO: write at tail, advance tail with wrap-around, update count.
            if (_count >= _items.Length) {
                throw new InvalidOperationException("The queue is full.");
            }

            if(_count <= 0) {
                _head = 0;
                _tail = 0;
            } else {
                // Wrap around, to ensure that index out of bounds doesn't happen
                _tail = (_tail + 1) % _items.Length;
            }
            _items[_tail] = item;
            _count++;
        }

        public T Peek() {
            // TODO: return the item at head without removing it.
            if(_count <= 0) {
                throw new InvalidOperationException("The queue is empty.");
            }
            return _items[_head];
        }

        /**
         * Why can ListQueue<T>.Dequeue require work proportional to the number of queued items?
         * Theoretically (since the List class is already optimized), because you need to shift
         * each element forward, so that's there's a head that the class can point to. 
         * 
         * Why does RingQueue<T>.Dequeue avoid shifting the remaining items?
         * Because only the pointers are edited, not the size of the array.
         * 
         * What trade-off does the fixed-capacity ring queue make?
         * In data structeres that have fixed-capacity, you may only have a certain total amount of elements.
         * 
         * Why is the abstraction "queue" different from the representation used to implement it?
         *  The user doesn't need to understand the implementation of the code, like keeping track of _head, 
         * _tail memory management or wrap-around math. The user only needs to understand what of a queue 
         * does (calling Enqueue and  Dequeue) and how it can be used, which definies a simpler abstraction. 
         * Also, since the implementation is hidden behind the abstraction, the developer can optimize or 
         * change the code without breaking external code.
         */
        public T Dequeue() {
            // TODO: read/clear at head, advance head with wrap-around, update count.
            if (_count <= 0) {
                throw new InvalidOperationException("The queue is empty.");
            }
            T returnValue = _items[_head];
            _items[_head] = default!; // Clear the reference for the Garbage Collector
            _head = (_head + 1) % _items.Length;
            _count--;
            if (_count <= 0) {
                _head = -1;
                _tail = -1;
            }
            return returnValue;
        }

        public void Clear() {
            /**
             * Using _items.Length is optimal for RingQueue due to the wrap-around math and elements aren't
             * always stored contiguously (from index 0 to _count - 1).
             */
            Array.Clear(_items, 0, _items.Length);
            /*for (int i = _count - 1; i >= 0; i--){
                _items[i] = default!;
            }*/
            _count = 0;
            _head = -1;
            _tail = -1;
        }
    }
}
