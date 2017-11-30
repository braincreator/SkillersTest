using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace SkillersTest.DataGenerator
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var fileName = @"../../../1.txt";
            var generator = new Generator();

            Stopwatch watch = new Stopwatch();
            watch.Start();
            //for (int i = 0; i < 1000; i++)
            //{
            //    var data = generator.GenerateNewDataItem();
            //    var s = i;
            //}
            var bytes = generator.Generate(1024 * 1024 * 1000);
            File.WriteAllBytes(fileName, bytes);
            var lines = File.ReadLines(fileName);
            watch.Stop();
            Console.WriteLine(watch.Elapsed);
            //File.Delete(fileName);
        }
    }
}
