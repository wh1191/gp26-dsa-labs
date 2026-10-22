using System;
using System.Linq;

namespace DSA.Lab01
{
    public static class SearchAlgorithms
    {
        /**
         * 1.
         * Q: What does `i` represent?
         * A: It represents the current index of the parameter 'values' array, so that the for loop can go through all the elements in the array.
         *
         * Q: When does the method stop early?
         * A: Whenever the for loop finds the correct value 'target' in the array 
         *
         * Q: What does `-1` mean?
         * A: The function returns -1 if the function couldn’t find the specific value 'target' in the array
         *
         * Q: What is the best case?
         * A: If the very first element is the correct 'target' value
         *
         * Q: What is the worst case?
         * A: That the function goes through all the elements without finding the 'target' value
         *
         * Q: If the array becomes twice as large, what happens to the worst-case amount of work?
         * A: It the amount of work will double (linear growth O(n))
         * 
         * - Why is array indexing treated as O(1)?
         * The computer doesn’t need to search or count through an array of elements to find an index, it calculates the exact memory location 
         * instantly (base address + (index * size of element)). And because it takes a number of operations (depending on how the code is written), 
         * regardless of array size, it is O(1).
         * 
         * - Why is linear search O(n) in the worst case?
         * Since linear search goes through elements one by one, if the target is at the last element of the array or not in the array at all, 
         * the algorithm must inspect all the elements. This means that the number of operations scales directly with O(n).
         */
        public static int LinearSearch(int[] values, int target)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            for (int i = 0; i < values.Length; ++i)
            {
                if (values[i] == target)
                {
                    return i;
                }
            }

            return -1;
        }

        /**
         * - Explain in one or two sentences why binary search requires ordered data.
         * Because if it's not sorted, there might be a case when the Binary Search will completely miss the 'target' value,
         * due to discarding one half of the elements at each step, in case the 'target' value might be in that discarded half.
         * Binary Search relies on a predictable sequence to mathematically eliminate half of the remaning items at each step,
         * that's why requires ordered data.
         * 
         * - Why is binary search O(log n)?
         * During each comparison, binary search cuts the needless search space in half (eliminates 50% of the possibilities 
         * on every step). For example, an array of 1 000 000 elements takes a maximum of about 20 steps to search (2^20 is 
         * approximately 1 000 000). 
         */
        public static int BinarySearch(int[] sortedValues, int target) {
            if (sortedValues == null) {
                throw new ArgumentNullException(nameof(sortedValues));
            }

            //maybe make use of a Sorting function here?

            /*if (sortedValues.Length > 0) {
                Console.WriteLine("Before Binary Search: " + string.Join(", ", sortedValues));
            }*/

            int low = 0;
            int high = sortedValues.Length - 1;

            while (low <= high) {
                int middle = low + ((high - low) / 2);

                //Console.WriteLine("Current values: [middle: " + middle + "], [low: " + low + "], [high: " + high + "]");

                if (sortedValues[middle] == target) {
                    //Console.WriteLine("Found target value!");
                    return middle;
                }

                if (sortedValues[middle] < target) {
                    //Console.WriteLine("Going to the right.");
                    low = middle + 1;
                }
                else {
                    //Console.WriteLine("Going to the left.");
                    high = middle - 1;
                }
            }
            //Console.WriteLine("Target value not found...");
            return -1;
        }
    }
}
