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
       
            Stopwatch watch = new Stopwatch();

            watch.Start();
            Sorter.QuicksortParallel(elems, 0, elems.Count - 1, comparer);
            watch.Stop();
            Console.WriteLine($"QuicksortParallel: {watch.Elapsed}");

         

            //watch.Restart();
            //Sorter.Quicksort(elems, 0, elems.Count - 1, comparer);
            //watch.Stop();
            //Console.WriteLine($"Quicksort: {watch.Elapsed}");

            for (int i = 480000; i < 500000; i++)
            {
                Console.WriteLine(elems[i]);
            }
        }


    }
}
