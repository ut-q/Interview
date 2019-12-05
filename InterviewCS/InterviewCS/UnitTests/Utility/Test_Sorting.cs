using System;
using System.Collections.Generic;
using System.Text;
using InterviewCS.Utility;

namespace InterviewCS.UnitTests.Utility
{
    public class Test_Sorting : ITestable
    {

        public void Test()
        {
            int[] a1 = {4, 5, 23, 7, 545, 7676, 34, 45, 7, 9, 1, 3, 878, 7, -1, 23, -9, 0, 5};
            int[] a2 = { };
            int[] a3 = {1};
            int[] a4 = {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
            int[] a5 = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            int[] a6 = {9, 8, 7, 6, 5, 4, 3, 2, 1, 0, -1, -2, -3, -4};
            int[] a7 = { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0, -1, -2, -3, -4, 11 };

            string[] s1 = {"A", "a", "abcde", "abced", "ABCDE", "", "0", "A"};

            Console.WriteLine("-------SELECTION SORT");
            Console.WriteLine(Sorting.SelectionSort(a1));
            Console.WriteLine(Sorting.SelectionSort(a2));
            Console.WriteLine(Sorting.SelectionSort(a3));
            Console.WriteLine(Sorting.SelectionSort(a4));
            Console.WriteLine(Sorting.SelectionSort(a5));
            Console.WriteLine(Sorting.SelectionSort(a6));
            Console.WriteLine(Sorting.SelectionSort(a7));
            Console.WriteLine(Sorting.SelectionSort(s1));

            Console.WriteLine("-------INSERTION SORT");
            Console.WriteLine(Sorting.InsertionSort(a1));
            Console.WriteLine(Sorting.InsertionSort(a2));
            Console.WriteLine(Sorting.InsertionSort(a3));
            Console.WriteLine(Sorting.InsertionSort(a4));
            Console.WriteLine(Sorting.InsertionSort(a5));
            Console.WriteLine(Sorting.InsertionSort(a6));
            Console.WriteLine(Sorting.InsertionSort(a7));
            Console.WriteLine(Sorting.InsertionSort(s1));

            Console.WriteLine("-------BUBBLE SORT");
            Console.WriteLine(Sorting.BubbleSort(a1));
            Console.WriteLine(Sorting.BubbleSort(a2));
            Console.WriteLine(Sorting.BubbleSort(a3));
            Console.WriteLine(Sorting.BubbleSort(a4));
            Console.WriteLine(Sorting.BubbleSort(a5));
            Console.WriteLine(Sorting.BubbleSort(a6));
            Console.WriteLine(Sorting.BubbleSort(a7));
            Console.WriteLine(Sorting.BubbleSort(s1));

            Console.WriteLine("-------QUICK SORT");
            Console.WriteLine(Sorting.QuickSort(a1));
            Console.WriteLine(Sorting.QuickSort(a2));
            Console.WriteLine(Sorting.QuickSort(a3));
            Console.WriteLine(Sorting.QuickSort(a4));
            Console.WriteLine(Sorting.QuickSort(a5));
            Console.WriteLine(Sorting.QuickSort(a6));
            Console.WriteLine(Sorting.QuickSort(a7));
            Console.WriteLine(Sorting.QuickSort(s1));

            Console.WriteLine("-------MERGE SORT");
            Console.WriteLine(Sorting.MergeSort(a1));
            Console.WriteLine(Sorting.MergeSort(a2));
            Console.WriteLine(Sorting.MergeSort(a3));
            Console.WriteLine(Sorting.MergeSort(a4));
            Console.WriteLine(Sorting.MergeSort(a5));
            Console.WriteLine(Sorting.MergeSort(a6));
            Console.WriteLine(Sorting.MergeSort(a7));
            Console.WriteLine(Sorting.MergeSort(s1));

            Console.WriteLine("-------HEAP SORT");
            Console.WriteLine(Sorting.HeapSort(a1));
            Console.WriteLine(Sorting.HeapSort(a2));
            Console.WriteLine(Sorting.HeapSort(a3));
            Console.WriteLine(Sorting.HeapSort(a4));
            Console.WriteLine(Sorting.HeapSort(a5));
            Console.WriteLine(Sorting.HeapSort(a6));
            Console.WriteLine(Sorting.HeapSort(a7));
            Console.WriteLine(Sorting.HeapSort(s1));
        }
    }
}
