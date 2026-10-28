using System;
using System.Collections.Generic;

namespace DSA.Lab03
{
    public sealed class IntHashTable<TValue> {
        private readonly List<Entry<TValue>>[] _buckets;
        private int _count;

        public IntHashTable(int bucketCount) {
            if (bucketCount <= 0) {
                throw new ArgumentOutOfRangeException(nameof(bucketCount));
            }

            _buckets = new List<Entry<TValue>>[bucketCount];
            for (int i = 0; i < _buckets.Length; i++) {
                _buckets[i] = new List<Entry<TValue>>();
            }
        }

        public int Count => _count;
        public int BucketCount => _buckets.Length;

        public void Set(int key, TValue value) {
            // TODO: search only the correct bucket, replace or append.
            if (key < 0) {
                throw new ArgumentOutOfRangeException(nameof(key), "This learning implementation uses non-negative integer keys.");
            }

            int bucketIndex = GetBucketIndex(key);

            if (_buckets[bucketIndex] == null) {
                throw new InvalidOperationException("One of the elements in '_buckets' is null.");
            }

            for (int i = 0; i < _buckets[bucketIndex].Count; i++) {
                if (_buckets[bucketIndex][i].Key == key) {
                    _buckets[bucketIndex][i] = new Entry<TValue>(key, value);
                    return;
                }
            }

            _buckets[bucketIndex].Add(new Entry<TValue>(key, value));
            _count++;
        }

        public bool TryGetValue(int key, out TValue value) {
            // TODO: search only the correct bucket.
            value = default!;

            if (key < 0) {
                throw new ArgumentOutOfRangeException(nameof(key), "This learning implementation uses non-negative integer keys.");
            }

            int bucketIndex = GetBucketIndex(key);

            /*for (int i = 0; i < _buckets[bucketIndex].Count; i++) {
                if (_buckets[bucketIndex][i].Key == key) {
                    return true;
                } 
            }*/
            foreach (Entry<TValue> entry in _buckets[bucketIndex]) {
                if (entry.Key == key) {
                    value = entry.Value;
                    return true;
                }
            }

            return false;
        }

        public bool ContainsKey(int key) {
            // TODO: reuse the lookup behavior if useful.
            if (key < 0) {
                throw new ArgumentOutOfRangeException(nameof(key), "This learning implementation uses non-negative integer keys.");
            }

            int bucketIndex = GetBucketIndex(key);

            foreach (Entry<TValue> entry in _buckets[bucketIndex]) {
                if (entry.Key == key) {
                    return true;
                } 
            }

            return false;
        }

        /**
         * Which keys collide?
         * A collision happens when two or more different keys end up assigned or added to the exact same bucket.
         * 
         * Keys 101 and 206 collide in Bucket 1
         * Keys 734 and 999 collide in Bucket 4
         * 
         * 101 / 5 = 20 with a remainder of 1 -> Bucket 1
         * 206 / 5 = 41 with a remainder of 1 -> Bucket 1
         * 
         * 734 / 5 = 146 with a remainder of 4 -> Bucket 4
         * 999 / 5 = 199 with a remainder of 4 -> Bucket 4
         */
        private int GetBucketIndex(int key) {
            if (_buckets == null) {
                throw new InvalidOperationException("'_buckets' is null.");
            }

            if (key < 0) {
                throw new ArgumentOutOfRangeException(nameof(key), "This learning implementation uses non-negative integer keys.");
            }

            return key % _buckets.Length;
        }
    }
}
