using System;
using System.Text;
using InterviewCS.DataStructures.Dictionary;

namespace InterviewCS.UnitTests.DataStructures
{
    public class Test_SortedDictionary  : ITestable
    {
        public void Test()
        {
            var dict = new SortedDictionary<int, int>();

            dict.Add(11, 1);
            dict.Add(2, 1);
            dict.Add(-3, 1);
            dict.Add(4, 5);
            dict.Add(5, 1);
            dict.Add(6, 2);
            dict.Add(76, 1);
            dict.Add(8, 1);
            dict.Add(9, -1);
            dict.Add(18, 1);
            dict.Add(12, 1);
            dict.Add(19, -1);

            Console.WriteLine(dict);
            
            dict[1] = 2;
            dict[11] = Int32.MinValue;

            Console.WriteLine(dict);
           
            Console.WriteLine("Contains 7 " + dict.ContainsKey(7));
            dict.Remove(7);
            Console.WriteLine("Contains 7 " + dict.ContainsKey(7));
           

            Console.WriteLine("Contains value 2 " + dict.ContainsValue(2));
            Console.WriteLine("Contains value 3 " + dict.ContainsValue(3));
        }
    }
}
