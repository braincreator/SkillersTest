using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillersTest.DataSorter
{
    public class Sorter
    {
        public Sorter()
        {
           
        }

        public static void InsertionSort<T>(IList<T> elements, Comparer<T> comparer)
        {
            for (var counter = 0; counter < elements.Count - 1; counter++)
            {
                var index = counter + 1;
                while (index > 0)
                {
                    if (comparer.Compare(elements[index - 1], elements[index]) > 0)
                    {
                        var temp = elements[index - 1];
                        elements[index - 1] = elements[index];
                        elements[index] = temp;
                    }
                    index--;
                }
            }
        }

        /// <summary>
        ///     Realizes a Quicksort on an IList of items using Comparer in a sequential way.
        /// </summary>
        /// <param name="elements">The IList of IComaparable to Quicksort</param>
        /// <param name="left">The minimum index of the IList to Quicksort</param>
        /// <param name="right">The maximum index of the IList to Quicksort</param>
        public static void Quicksort<T>(IList<T> elements, int left, int right, Comparer<T> comparer)
        {
            int i = left, j = right;
            T pivot = elements[(left + right) / 2];

            while (i <= j)
            {
                while (comparer.Compare(elements[i], pivot) < 0)
                {
                    i++;
                }

                while (comparer.Compare(elements[j], pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    T tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            if (left < j)
            {
                Quicksort(elements, left, j, comparer);
            }

            if (i < right)
            {
                Quicksort(elements, i, right, comparer);
            }
        }





        /// <summary>
        ///     Swaps the two values of the specified indexes
        /// </summary>
        /// <param name="arr">An IList where elements need to be swapped</param>
        /// <param name="i">The first index to be swapped</param>
        /// <param name="j">The second index to be swapped</param>
        private static void Swap<T>(IList<T> arr, int i, int j)
        {
            var tmp = arr[i];
            arr[i] = arr[j];
            arr[j] = tmp;
        }


        /// <summary>
        ///     Partitions an IList by defining a pivot and then comparing the other members to this pivot.
        /// </summary>
        /// <param name="arr">The IList to partition</param>
        /// <param name="low">The lowest index of the partition</param>
        /// <param name="high">The highest index of the partition</param>
        /// <returns>Returns the index of the chosen pivot</returns>
        private static int Partition<T>(IList<T> arr, int low, int high, Comparer<T> comparer)
        {
            /*
                * Defining the pivot position, here the middle element is used but the choice of a pivot
                * is a rather complicated issue. Choosing the left element brings us to a worst-case performance,
                * which is quite a common case, that's why it's not used here.
                */
            var pivotPos = (high + low) / 2;
            var pivot = arr[pivotPos];
            // Putting the pivot on the left of the partition (lowest index) to simplify the loop
            Swap(arr, low, pivotPos);

            /** The pivot remains on the lowest index until the end of the loop
                * The left variable is here to keep track of the number of values
                * that were compared as "less than" the pivot.
                */
            var left = low;
            for (var i = low + 1; i <= high; i++)
            {
                // If the value is greater than the pivot value we continue to the next index.
                if (comparer.Compare(arr[i], pivot) >= 0) continue;

                // If the value is less than the pivot, we increment our left counter (one more element below the pivot)
                left++;
                // and finally we swap our element on our left index.
                Swap(arr, i, left);
            }

            // The pivot is still on the lowest index, we need to put it back after all values found to be "less than" the pivot.
            Swap(arr, low, left);

            // We return the new index of our pivot
            return left;
        }


        ///// <summary>
        /////     Realizes a Quicksort on an IList of items using Comparer in a sequential way.
        ///// </summary>
        ///// <param name="arr">The IList of items to Quicksort</param>
        ///// <param name="left">The minimum index of the IList to Quicksort</param>
        ///// <param name="right">The maximum index of the IList to Quicksort</param>
        //public static void Quicksort<T>(IList<T> arr, int left, int right, Comparer<T> comparer)
        //{
        //    // If the list contains one or less element: no need to sort!
        //    if (right <= left) return;

        //    // Partitioning our list
        //    var pivot = Partition(arr, left, right, comparer);

        //    // Sorting the left of the pivot
        //    Quicksort(arr, left, pivot - 1, comparer);
        //    // Sorting the right of the pivot
        //    Quicksort(arr, pivot + 1, right, comparer);
        //}


        /// <summary>
        ///     Realizes a Quicksort on an IList of IComparable items.
        ///     Left and right side of the pivot are processed in parallel.
        /// </summary>
        /// <param name="arr">The IList of IComaparable to Quicksort</param>
        /// <param name="left">The minimum index of the IList to Quicksort</param>
        /// <param name="right">The maximum index of the IList to Quicksort</param>
        public static void QuicksortParallel<T>(IList<T> arr, int left, int right, Comparer<T> comparer)
        {
            // Defining a minimum length to use parallelism, over which using parallelism
            // got better performance than the sequential version.
            const int threshold = 2048;

            // If the list to sort contains one or less element, the list is already sorted.
            if (right <= left) return;

            // If the size of the list is under the threshold, sequential version is used.
            if (right - left < threshold)
                Quicksort(arr, left, right, comparer);

            else
            {
                // Partitioning our list and getting a pivot.
                var pivot = Partition(arr, left, right, comparer);

                // Sorting the left and right of the pivot in parallel
                Parallel.Invoke(
                    () => QuicksortParallel(arr, left, pivot - 1, comparer),
                    () => QuicksortParallel(arr, pivot + 1, right, comparer));
            }
        }
    
    }
}
