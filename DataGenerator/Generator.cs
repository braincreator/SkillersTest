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
            return MainStringBuilder.AppendLine(Rnd.Next(MinNumber, MaxNumber) < MaxNumber / 2 ? Fruits[Rnd.Next(0, Fruits.Length - 1)] : $"{Fruits[Rnd.Next(0, Fruits.Length - 1)]} is {Properties[Rnd.Next(0, Properties.Length - 1)]}").ToString();
        }

        public byte[] Generate(Int64 desiredArraySize)
        {
            Int64 currentSize = 0;
            byte[] output;

            using (MemoryStream stream = new MemoryStream())
            {
                while (currentSize < desiredArraySize)
                {
                    var item = GenerateNewDataItem();
                    var bytes = Encoding.Default.GetBytes(item);
                    var size = bytes.Length;
                    currentSize += size;
                    stream.Write(bytes, 0, size);
                }
                output = stream.ToArray();
            }
            return output;
        }
    }
}
