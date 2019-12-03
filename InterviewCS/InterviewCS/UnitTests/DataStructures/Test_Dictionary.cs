using System;
using InterviewCS.DataStructures.Dictionary;
using System.Text;

namespace InterviewCS.UnitTests.DataStructures
{
    public class Test_Dictionary : ITestable
    {
        public void Test()
        {
            var dict = new Dictionary<int, int>();

            dict.Add(1,1);
            dict.Add(2, 1);
            dict.Add(3, 1);
            dict.Add(4, 1);
            dict.Add(5, 1);
            dict.Add(6, 1);
            dict.Add(7, 1);
            dict.Add(8, 1);
            dict.Add(9, 1);
            dict.Add(10, 1);
            dict.Add(11, 1); 
            dict.Add(19, 1);

            Console.WriteLine(dict);
            Console.WriteLine("Count: " + dict.Count + " Load Factor: " + dict.LoadFactor + " MaxLF: " + dict.MaxLoadFactor);

            dict[1] = 2;

            Console.WriteLine(dict);
            Console.WriteLine("Count: " + dict.Count + " Load Factor: " + dict.LoadFactor + " MaxLF: " + dict.MaxLoadFactor);

            Console.WriteLine("Contains 7 " + dict.ContainsKey(7));
            dict.Remove(7);
            Console.WriteLine("Contains 7 " + dict.ContainsKey(7));
            Console.WriteLine("Count: " + dict.Count + " Load Factor: " + dict.LoadFactor + " MaxLF: " + dict.MaxLoadFactor);


            Console.WriteLine("Contains value 2 " + dict.ContainsValue(2));
            Console.WriteLine("Contains value 3 " + dict.ContainsValue(3));

            

        }
    }
}
