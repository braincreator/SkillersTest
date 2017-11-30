using System;
namespace SkillersTest.Common
{
    public class DataItem
    {
        public int Number;
        public string Text = String.Empty;

        public override string ToString()
        {
            return $"{Number}. {Text}";
        }

        public static DataItem FromString(string input)
        {
            DataItem item = new DataItem();
            var textStartIndex = input.IndexOf(' ') + 1;
            var numberLastIndex = input.IndexOf('.');
            item.Number = int.Parse(input.Substring(0, numberLastIndex));
            item.Text = input.Substring(textStartIndex, input.Length - textStartIndex);//.Replace("\n", "");
            return item;
        }
    }
}
