using System;
using System.Collections.Generic;

namespace SkillersTest.DataSorter
{
    public class Sorter
    {
        public Sorter()
        {
           
        }

        public static T[] InsertionSort<T>(T[] elements, Comparer<T> comparer)
        {
            for (var counter = 0; counter < elements.Length - 1; counter++)
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
            return elements;
        }

        public static void Quicksort<T>(T[] elements, int left, int right, Comparer<T> comparer)
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

        public static IEnumerable<T> MergeSort<T>(T[] list, int left, int right, Comparer<T> comparer)
        {
            if (left == right)
            {
                return SingleValue(list[left]);
            }

            int mid = (left + right) / 2;
            var firstEnumerable = MergeSort(list, left, mid, comparer);
            var secondEnumerable = MergeSort(list, mid + 1, right, comparer);
            return Merge(firstEnumerable, secondEnumerable, comparer);
        }

        private static IEnumerable<T> SingleValue<T>(T value)
        {
            yield return value;
        } 

        private static IEnumerable<T> Merge<T>(IEnumerable<T> firstEnumerable, IEnumerable<T> secondEnumerable, Comparer<T> comparer)
        {
            using (var firstEnumerator = firstEnumerable.GetEnumerator())
            using (var secondEnumerator = secondEnumerable.GetEnumerator())
            {
                bool first = firstEnumerator.MoveNext();
                bool second = secondEnumerator.MoveNext();

                while (first && second)
                {
                    if (comparer.Compare(firstEnumerator.Current, secondEnumerator.Current) < 0)
                    {
                        yield return firstEnumerator.Current;
                        first = firstEnumerator.MoveNext();
                    }
                    else
                    {
                        yield return secondEnumerator.Current;
                        second = secondEnumerator.MoveNext();
                    }
                }

                while (first)
                {
                    yield return firstEnumerator.Current;
                    first = firstEnumerator.MoveNext();
                }

                while (second)
                {
                    yield return secondEnumerator.Current;
                    second = secondEnumerator.MoveNext();
                }
            }
        }
    }
}
