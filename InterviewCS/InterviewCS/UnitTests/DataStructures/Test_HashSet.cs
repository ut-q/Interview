using System;
using InterviewCS.DataStructures.HashSet;
using System.Text;

namespace InterviewCS.UnitTests.DataStructures
{
    public class Test_HashSet : ITestable
    {
        public void Test()
        {
            var h = new HashSet<int>();

            h.Add(1);
            h.Add(5);
            h.Add(6);
            h.Add(9);
            h.Add(15);
            h.Add(6);
            h.Add(4);
            h.Add(0);
            h.Add(-5);
            h.Add(999);
            h.Add(3); 
            h.Add(5); 
            h.Add(9); 
            h.Add(2); 
            h.Add(8); 
            h.Add(12); 
            h.Add(25);
            h.Add(17);
            h.Add(17);
            h.Add(13);

            Console.WriteLine(h);
            Console.WriteLine("Count: " + h.Count + " Load Factor: " + h.LoadFactor + " MaxLF: " + h.MaxLoadFactor);

            h.Remove(-5);

            h.ExceptWith(new int[] {9,2,999});
            Console.WriteLine(h);
            Console.WriteLine("Count: " + h.Count + " Load Factor: " + h.LoadFactor + " MaxLF: " + h.MaxLoadFactor);

            h.IntersectWith(new int[] {-1,-2,-9,-3});

            Console.WriteLine(h);
            Console.WriteLine("Count: " + h.Count + " Load Factor: " + h.LoadFactor + " MaxLF: " + h.MaxLoadFactor);

            Console.WriteLine("Overlaps With {-10,-11,-12,-13} " + h.Overlaps(new int[]{ -10,-11,-12,-13}));


            Console.WriteLine("Overlaps With {3,5,6,7,8} " + h.Overlaps(new int[] { 3, 5, 6, 7, 8 }));

            h.UnionWith(new int[] {-10,-11,-12,-13});
            Console.WriteLine(h);
            Console.WriteLine("Count: " + h.Count + " Load Factor: " + h.LoadFactor + " MaxLF: " + h.MaxLoadFactor);

            h.SymmetricExceptWith(new int[] { -10, -11, -12, -13 });
            Console.WriteLine(h);
            Console.WriteLine("Count: " + h.Count + " Load Factor: " + h.LoadFactor + " MaxLF: " + h.MaxLoadFactor);


        }
    }
}
