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
            var inputFileName = @"../../../1.txt";
            var outputFileName = @"../../../2.txt";

            var comparer = Comparer<DataItem>.Create((DataItem x, DataItem y) =>
            {
                int result = string.Compare(x.Text, y.Text, StringComparison.Ordinal);
                if (result == 0)
                {
                    result = x.Number.CompareTo(y.Number);
                }
                return result;
            });

            //Test();

            HugeFileSort sorter = new HugeFileSort();
            sorter.Comparer = comparer;

            Stopwatch watch = new Stopwatch();

            Console.WriteLine("Sorting Started!");
            watch.Start();
            sorter.Sort(inputFileName, outputFileName);
            watch.Stop();
            Console.WriteLine($"Sorting time: {watch.Elapsed}");

            //for (int i = 480000; i < 500000; i++)
            //{
            //    Console.WriteLine(elems[i]);
            //}
        }

        public static void Test()
        {
            var inputFileName = @"../../../1.txt";
            var outputFileName = @"../../../2.txt";

            using (var reader = new StreamReader(inputFileName))
            {
                reader.BaseStream.Position = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                        if (line.Contains("\0"))
                        {
                            Console.WriteLine(line);
                        }
                }
            }
            Console.WriteLine("Test finished!");
        }

    }
}
