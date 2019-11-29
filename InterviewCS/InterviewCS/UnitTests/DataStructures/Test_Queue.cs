using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewCS.UnitTests.DataStructures
{
    public class Test_Queue : ITestable
    {
        public void Test()
        {
            InterviewCS.DataStructures.Queue.Queue<int> s1 = new InterviewCS.DataStructures.Queue.Queue<int>();

            Console.WriteLine("Peek at an empty queue " + s1.Peek());

            Console.WriteLine("Dequeue from empty stack " + s1.Dequeue());

            s1.Enqueue(1);
            s1.Enqueue(2);
            s1.Enqueue(3);
            s1.Enqueue(4);
            s1.Enqueue(5);
            s1.Enqueue(6);
            s1.Enqueue(7);

            Console.WriteLine(s1);

            Console.WriteLine("Count: " + s1.Count);

            Console.WriteLine("Peek " + s1.Peek());

            Console.WriteLine("Pop " + s1.Dequeue());

            s1.Dequeue();
            s1.Dequeue();

            Console.WriteLine(s1);

            s1.Clear();
            s1.Enqueue(1000);
            Console.WriteLine(s1);
        }
    }
}
