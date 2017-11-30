using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SkillersTest.Common;

namespace SkillersTest.DataSorter
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Sorter sorter = new Sorter();

            var fileName = @"../../../1.txt";
            var lines = File.ReadLines(fileName);
            List<DataItem> elems = new List<DataItem>();
            foreach (var line in lines)
            {
                elems.Add(DataItem.FromString(line));
            }
            var comparer = Comparer<DataItem>.Create((DataItem x, DataItem y) => {
                int result = string.Compare(x.Text, y.Text, StringComparison.Ordinal);
                if (result == 0)
                {
                    result = x.Number.CompareTo(y.Number);
                }
                return result;
            });
            var resultingArray = elems.ToArray(); 
            Stopwatch watch = new Stopwatch();

            //watch.Start();
            //Sorter.Quicksort(resultingArray, 0, elems.Count - 1, comparer);
            //watch.Stop();
            //Console.WriteLine($"QuickSort: {watch.Elapsed}");

            watch.Start();
            Sorter.MergeSort(resultingArray, 0, elems.Count - 1,  comparer);
            watch.Stop();
            Console.WriteLine($"MergeSort: {watch.Elapsed}");

            //watch.Restart();
            //Sorter.InsertionSort(resultingArray, comparer);
            //watch.Stop();
            //Console.WriteLine($"InsertionSort: {watch.Elapsed}");
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine(resultingArray[i]);
            }
        }


    }
}
