using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using InterviewCS.DataStructures.Heap;
using InterviewCS.DataStructures.Queue;
using Vector = InterviewCS.DataStructures.Vector;

namespace InterviewCS.Utility
{
    public class Sorting
    {
        public static Vector.Vector<T> SelectionSort<T>(IEnumerable<T> arr) where T: IComparable<T>
        {
            var vector = new Vector.Vector<T>(arr);
            SelectionSort(vector);

            return vector;
        }

        public static void SelectionSort<T>(Vector.Vector<T> vector) where T : IComparable<T>
        {
            for (int i = 0; i < vector.Count; ++i)
            {
                int min = i;
                for (int j = i + 1; j < vector.Count; ++j)
                {
                    // if min > j
                    if (vector[min].CompareTo(vector[j]) > 0)
                    {
                        min = j;
                    }
                }
                Swap(vector, i, min);
            }
        }

        public static Vector.Vector<T> InsertionSort<T>(IEnumerable<T> arr) where T : IComparable<T>
        {
            var vector = new Vector.Vector<T>(arr);
            InsertionSort(vector);

            return vector;
        }

        public static void InsertionSort<T>(Vector.Vector<T> vector) where T : IComparable<T>
        {
            for (int i = 0; i < vector.Count; ++i)
            {
                int j = i;
                //while j < j-1
                while (j > 0 && vector[j].CompareTo(vector[j - 1]) < 0)
                {
                    Swap(vector, j,j-1);
                    j--;
                }
            }
        }

        public static Vector.Vector<T> BubbleSort<T>(IEnumerable<T> arr) where T : IComparable<T>
        {
            var vector = new Vector.Vector<T>(arr);
            BubbleSort(vector);

            return vector;
        }

        public static void BubbleSort<T>(Vector.Vector<T> vector) where T : IComparable<T>
        {
            for (int i = 0; i < vector.Count - 1; ++i)
            {
                for (int j = 0; j < vector.Count - i - 1; ++j)
                {
                    if (vector[j].CompareTo(vector[j + 1]) > 0)
                    {
                        Swap(vector, j,j+1);
                    }
                }
            }
        }

        public static Vector.Vector<T> QuickSort<T>(IEnumerable<T> list) where T : IComparable<T>
        {
            var vector = new Vector.Vector<T>(list);
            QuickSort(vector);

            return vector;
        }

        public static void QuickSort<T>(Vector.Vector<T> vec) where T : IComparable<T>
        {
            QuickSort_Internal(vec, 0, vec.Count - 1);
        }

        public static Vector.Vector<T> HeapSort<T>(IEnumerable<T> arr) where T : IComparable<T>
        {
            var heap = new Heap<T>(arr);

            return heap.HeapSort();
        }

        public static Vector.Vector<T> MergeSort<T>(IEnumerable<T> arr) where T : IComparable<T>
        {
            var vector = new Vector.Vector<T>(arr);
            MergeSort(vector);
            return vector;
        }

        public static void MergeSort<T>(Vector.Vector<T> arr) where T : IComparable<T>
        {
            MergeSort_Internal(arr, 0, arr.Count - 1);
        }

        private static void MergeSort_Internal<T>(Vector.Vector<T> vec, int low, int high) where T : IComparable<T>
        {
            if (low < high)
            {
                int middle = (low + high) / 2;
                MergeSort_Internal<T>(vec, low, middle);
                MergeSort_Internal<T>(vec, middle + 1, high);
                Merge(vec, low, middle, high);
            }
        }

        private static void Merge<T>(Vector.Vector<T> vec, int low, int middle, int high) where T: IComparable<T>
        {
            DataStructures.Queue.Queue<T> q1 = new DataStructures.Queue.Queue<T>();
            DataStructures.Queue.Queue<T> q2 = new DataStructures.Queue.Queue<T>();

            for (int i = low; i <= middle; ++i)
            {
                q1.Enqueue(vec[i]);
            }

            for (int i = middle + 1; i <= high; ++i)
            {
                q2.Enqueue(vec[i]);
            }

            int index = low;

            while (q1.Count > 0 && q2.Count > 0)
            {
                if (q1.Peek().CompareTo(q2.Peek()) < 0)
                {
                    vec[index++] = q1.Dequeue();
                }
                else
                {
                    vec[index++] = q2.Dequeue();
                }
            }

            while (q1.Count > 0)
            {
                vec[index++] = q1.Dequeue();
            }

            while (q2.Count > 0)
            {
                vec[index++] = q2.Dequeue();
            }
        }

        private static void QuickSort_Internal<T>(Vector.Vector<T> vec, int low, int high) where T : IComparable<T>
        {
            if (low < high)
            {
                int pivot = Partition(vec, low, high);
                QuickSort_Internal(vec, low, pivot - 1);
                QuickSort_Internal(vec, pivot + 1, high);
            }
        }

        private static int Partition<T>(Vector.Vector<T> vec, int low, int high) where T : IComparable<T>
        {
            int pivot = high; // can be picked randomly too
            int minHigh = low;

            for (int i = low; i < high; ++i)
            {
                if (vec[i].CompareTo(vec[pivot]) < 0)
                {
                    Swap(vec, minHigh, i);
                    minHigh++;
                }
            }

            Swap(vec, pivot, minHigh);

            return minHigh;
        }

        private static void Swap<T>(Vector.Vector<T> vector, int a, int b)
        {
            if (a >= 0 && a < vector.Count && b >= 0 && b < vector.Count)
            {
                T temp = vector[a];
                vector[a] = vector[b];
                vector[b] = temp;
            }
        }
    }
}
