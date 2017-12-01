using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SkillersTest.DataGenerator
{
    public class Generator
    {
        const int MinNumber = 1;
        const int MaxNumber = 1000000;
        int MaxRamUsage = 1024 * 1024 * 1000;

        string[] Fruits = { "Apple", "Banana", "Lemon", "Grape", "Orange", "Strawberry", "Kiwi" };
        string[] Properties = { "red", "yellow", "green", "blue", "orange", "big", "small", "the best", "godlike" };

        public Generator()
        {

        }

        StringBuilder MainStringBuilder = new StringBuilder();
        Random Rnd = new Random();

        public string GenerateNewDataItem()
        {
            MainStringBuilder.Clear();
            var number = Rnd.Next(MinNumber, MaxNumber);
            MainStringBuilder.Append(number).Append(". ");
            return MainStringBuilder.Append(Rnd.Next(MinNumber, MaxNumber) < MaxNumber / 2 ? Fruits[Rnd.Next(0, Fruits.Length - 1)] : $"{Fruits[Rnd.Next(0, Fruits.Length - 1)]} is {Properties[Rnd.Next(0, Properties.Length - 1)]}").ToString();
        }

        public void Generate(Int64 desiredArraySize, string fileName)
        {
            Int64 currentSize = 0;
            int tempSize = 0;
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                //sw.AutoFlush = false;
                using (MemoryStream stream = new MemoryStream())
                {
                    while (currentSize < desiredArraySize)
                    {
                        var item = GenerateNewDataItem();
                        if (item.Contains("\0"))
                        {
                            Console.WriteLine("Warning!");
                        }
                        var bytes = Encoding.Default.GetBytes(item);
                        var size = bytes.Length;
                        currentSize += size;
                        tempSize += size;
                        stream.Write(bytes, 0, size);

                        if (tempSize >= MaxRamUsage / 2)
                        {
                            writer.Write(stream.GetBuffer());
                            stream.SetLength(0);
                            //writer.Flush();
                            tempSize = 0;
                        }
                    }
                }
            }
        }
    }
}
