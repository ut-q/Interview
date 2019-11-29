using System;
using System.Collections.Generic;
using System.Text;
using InterviewCS.DataStructures.Heap;

namespace InterviewCS.UnitTests.DataStructures
{
    public class Test_Heap : ITestable
    {

        public void Test()
        {
            var h1 = new Heap<int>();

            h1.Insert(6);
            h1.Insert(7);
            h1.Insert(11);
            h1.Insert(5);
            h1.Insert(6);
            h1.Insert(9);
            h1.Insert(1);
            h1.Insert(3);
            h1.Insert(4);

            Console.WriteLine(h1);

            h1.RemoveAt(4);

            Console.WriteLine(h1);

            Console.WriteLine("Sorted heap: " + h1.HeapSort());

            Console.WriteLine("Heap after: " + h1);

            var h2 = new Heap<int>(new int[]{5,3,2,7,8,9,3,6,1,8});

            Console.WriteLine(h2);
            Console.WriteLine(h2.HeapSort());

            var h3 = new Heap<string>(new string[]{"a","b","c","abcde","h","y","dsffdsfds", " test"});

            Console.WriteLine(h3);
            Console.WriteLine(h3.HeapSort());
        }
    }
}
