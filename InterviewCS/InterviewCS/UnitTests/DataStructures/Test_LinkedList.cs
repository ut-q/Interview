using System;
using System.Text;
using InterviewCS.DataStructures.LinkedList;

namespace InterviewCS.UnitTests.DataStructures
{
    public class Test_LinkedList
    {

        public void Test()
        {
            LinkedList<int> list1 = new LinkedList<int>();

            Console.WriteLine("Count: " + list1.Count);

            list1.AddFirst(1);
            list1.AddFirst(0);
            list1.AddFirst(-1);

            list1.AddLast(2);
            list1.AddLast(4);

            var node = list1.Find(4);

            list1.AddBefore(node, 3);
            Console.WriteLine(list1.ToString());
            list1.AddAfter(node, 5);

            Console.WriteLine(list1.ToString());

            Console.WriteLine("List contains 2 - " + (list1.Contains(2) ? "TRUE" : "FALSE"));

            Console.WriteLine("List contains 6 - " + (list1.Contains(6) ? "TRUE" : "FALSE"));

            list1.Remove(0);
            list1.RemoveFirst();
            list1.RemoveFirst();
            list1.RemoveLast();

            Console.WriteLine("Count: " + list1.Count);

            Console.WriteLine(list1.ToString());

            LinkedList<string> list2 = new LinkedList<string>(new string[]{"abcde", "abcdefg", "bcd"});

            Console.WriteLine(list2.ToString());
        }

    }
}
