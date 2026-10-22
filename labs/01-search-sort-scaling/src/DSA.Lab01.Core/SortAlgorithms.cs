using System;
using System.Linq;

namespace DSA.Lab01
{
    public static class SortAlgorithms
    {

        /**
         * - Why is this insertion-sort implementation is O(n²) in the worst case?
         * If the unsorted array happens to be “sorted” but in reverse order, every new element picked from the unsorted section 
         * must be compared and shifted backward past every single element already in the sorted section. Since the code has 
         * nested loops, the number of operations will become quadratic.
         * 
         * - Why should you be cautious about drawing conclusions from one timing run?
         * External factors like other background operating system processes, memory caching etc may cause different results. 
         * Multiple runs are needed to average out the data.
         */
        public static void InsertionSort(int[] values) {
            if (values == null) {
                throw new ArgumentNullException(nameof(values));
            }

            /*if(values.Length > 0) {
                Console.WriteLine("Before insertion sort: " + string.Join(", ", values));
            }*/

            // Start from the second element (i = 1) assuming the first is already "sorted"
            for (int i = 1; i < values.Length; i++) {
                int insertValue = values[i];
                int j = i - 1;

                // If a sorted element is greater than 'insertValue', then it shifts that element one spot to the right
                while (j >= 0 && values[j] > insertValue) {
                    values[j + 1] = values[j];
                    j--;
                }

                // If it finds an element smaller than 'insertValue', insert that value into its correct position
                values[j + 1] = insertValue;
                //Console.WriteLine("Step " + i + ": " + string.Join(", ", values));
            }
        }
    }
}
