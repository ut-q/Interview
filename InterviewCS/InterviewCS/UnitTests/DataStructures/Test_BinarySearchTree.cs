using System;
using System.Collections.Generic;
using System.Text;
using InterviewCS.DataStructures.Tree;
using InterviewCS.DataStructures.Vector;

namespace InterviewCS.UnitTests.DataStructures
{
    public class Test_BinarySearchTree : ITestable
    { 
        public void Test()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();

            tree.Insert(5);
            tree.Insert(8);
            tree.Insert(3);
            tree.Insert(8);
            tree.Insert(5);
            tree.Insert(7);
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(9);
            tree.Insert(12);

            Console.WriteLine(tree);

            Console.WriteLine(new Vector<int>(tree.ToArray()));
            Console.WriteLine(new Vector<int>(tree.ToSortedArray()));

            Console.WriteLine("Count is: " + tree.Count);
            Console.WriteLine("Minimum: " + tree.Minimum() + " Maximum: " + tree.Maximum());

            Console.WriteLine("Contains 7 " + tree.Contains(7));

            Console.WriteLine("Remove 7");
            tree.Remove(7);

            Console.WriteLine(tree);

            Console.WriteLine("Count is: " + tree.Count);
            Console.WriteLine("Minimum: " + tree.Minimum() + " Maximum: " + tree.Maximum());

            Console.WriteLine("Contains 7 " + tree.Contains(7));

            Console.WriteLine("Remove 1");
            tree.Remove(1);

            Console.WriteLine(tree);

            Console.WriteLine("Count is: " + tree.Count);
            Console.WriteLine("Minimum: " + tree.Minimum() + " Maximum: " + tree.Maximum());

            Console.WriteLine("Contains 1 " + tree.Contains(1));

            tree = new BinarySearchTree<int>(new int[]{5,4,3,2,8,7,5});

            Console.WriteLine(tree);

            Console.WriteLine(new Vector<int>(tree.ToArray()));
            Console.WriteLine(new Vector<int>(tree.ToSortedArray()));

            Console.WriteLine("Count is: " + tree.Count);
            Console.WriteLine("Minimum: " + tree.Minimum() + " Maximum: " + tree.Maximum());

            tree.Clear();


            Console.WriteLine("After clear " + tree);
        }
    }
}
