using System;
using System.Text;
using InterviewCS.DataStructures.Vector;

namespace InterviewCS.UnitTests.DataStructures
{
    public class Test_Vector : ITestable
    {
        public void Test()
        {
            Vector<int> v1 = new Vector<int>();

            Console.WriteLine("Capacity: " + v1.Capacity + " Count: " + v1.Count);

            v1.Add(1);
            v1.Add(2);
            v1.Add(4);

            Console.WriteLine(v1);

            Console.WriteLine("Capacity: " + v1.Capacity + " Count: " + v1.Count);

            v1.Insert(2,3);
            Console.WriteLine(v1);

            v1.Remove(2);

            v1.ForEach((item) =>
            {
                v1.Add(item*2);
                v1.Remove(item);
            });

            Console.WriteLine(v1);

            v1.Insert(1,4);

            v1.Add(8);

            Console.WriteLine("Finding an 8" );
            Console.WriteLine(v1.Find(x => x == 8));

            Console.WriteLine(v1.FindAll(x => x < 8));

            Console.WriteLine(v1.FindIndex(x => x>4));

            Console.WriteLine(v1.IndexOf(2));

            Console.WriteLine("List contains 2 - " + (v1.Contains(2) ? "TRUE" : "FALSE"));

            Console.WriteLine("List contains 6 - " + (v1.Contains(6) ? "TRUE" : "FALSE"));

            Console.WriteLine(v1);
            v1.Reverse();
            Console.WriteLine(v1);
            Console.WriteLine("Capacity: " + v1.Capacity + " Count: " + v1.Count);
            v1.TrimExcess();
            Console.WriteLine("Capacity: " + v1.Capacity + " Count: " + v1.Count);
            v1.Clear();
            Console.WriteLine("Capacity: " + v1.Capacity + " Count: " + v1.Count);
            Console.WriteLine(v1);

            Vector<int> v2 = new Vector<int>(2);

            Console.WriteLine("Capacity: " + v2.Capacity + " Count: " + v2.Count);

            v2.Add(1);
            v2.Add(2);
            v2.Add(4);
            Console.WriteLine(v2);
            Console.WriteLine("Capacity: " + v2.Capacity + " Count: " + v2.Count);
            Vector<string> v3 = new Vector<string>(new string[] { "abcde", "abcdefg", "bcd" });

            Console.WriteLine(v3);
        }
    }
}
