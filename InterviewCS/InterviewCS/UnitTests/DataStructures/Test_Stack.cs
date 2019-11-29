using System;
using System.Collections;
using System.Runtime.Intrinsics;
using InterviewCS.DataStructures.Stack;

namespace InterviewCS.UnitTests.DataStructures
{
    public class Test_Stack : ITestable
    {
        public void Test()
        {
            Stack<int> s1 = new Stack<int>();

            Console.WriteLine("Peek at an empty stack " + s1.Peek());

            Console.WriteLine("Pop from empty stack " + s1.Pop());

            s1.Push(1);
            s1.Push(2);
            s1.Push(3);
            s1.Push(4);
            s1.Push(5);
            s1.Push(6);
            s1.Push(7);

            Console.WriteLine(s1);

            Console.WriteLine("Count: " + s1.Count);

            Console.WriteLine("Peek " + s1.Peek());

            Console.WriteLine("Pop " + s1.Pop());

            s1.Pop();
            s1.Pop();

            Console.WriteLine(s1);

            s1.Clear();
            s1.Push(1000);
            Console.WriteLine(s1);
        }
    }
}
