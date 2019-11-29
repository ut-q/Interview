using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using InterviewCS.DataStructures.Vector;

namespace InterviewCS.DataStructures.Heap
{
    //TODO make this for heaps that are not min heap as well

    public class Heap<T> : IEnumerable<T> where T: IComparable<T>
    {
        #region Properties and Constants

        public int Count => Data.Count;

        #endregion

        #region Constructors and Operators

        public Heap()
        {
            Data = new Vector<T>();
        }

        public Heap(IEnumerable<T> list)
        {
            Data = new Vector<T>(list);

            for (var i = Count - 1; i >= 0; --i)
            {
                BubbleDown(i);
            }
        }

        public Heap(int capacity)
        {
            Data = new Vector<T>(capacity);
        }

        #endregion

        #region Functions

        public void Clear()
        {
            Data.Clear();
        }

        public T ExtractFirst()
        {
            return RemoveAt(0);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Vector<T> HeapSort()
        {
            var vector = new Vector<T>();
            var n = Count;

            for (var i = 0; i < n; ++i)
            {
                vector.Add(ExtractFirst());
            }

            return vector;
        }

        public void Insert(T item)
        {
            Data.Add(item);

            BubbleUp(Count - 1);
        }

        public T RemoveAt(int index)
        {
            if (!IsValidIndex(index))
            {
                throw new ArgumentOutOfRangeException();
            }

            var item = Data[index];

            Data.Swap(index, Count - 1);

            Data.RemoveAt(Count - 1);

            BubbleDown(index);

            return item;
        }

        public void TrimExcess()
        {
            Data.TrimExcess();
        }

        public T[] ToArray()
        {
            return Data.ToArray();
        }

        public override string ToString()
        {
            return Data.ToString();
        }

        #endregion

        #region Private Members

        private Vector<T> Data { get; set; }
        private const int RootParent = -1;

        private static int ParentIndex(int index)
        {
            if (index == 0)
            {
                return RootParent;
            }
            return (index - 1) / 2;
        }

        private static int LeftChildIndex(int index)
        {
            return 2 * index + 1;
        }

        private static int RightChildIndex(int index)
        {
            return 2 * index + 2;
        }

        private void BubbleDown(int index)
        {
            //find the smallest of the parent and the children

            var minIndex = index;

            if (IsValidIndex(LeftChildIndex(index)))
            {
                minIndex = Data[minIndex].CompareTo(Data[LeftChildIndex(index)]) < 0 ? minIndex : LeftChildIndex(index);
            }

            if (IsValidIndex(RightChildIndex(index)))
            {
                minIndex = Data[minIndex].CompareTo(Data[RightChildIndex(index)]) < 0 ? minIndex : RightChildIndex(index);
            }

            if (minIndex != index)
            {
                Data.Swap(minIndex, index);
                BubbleDown(minIndex);
            }
        }

        private void BubbleUp(int index)
        {
            if (IsRoot(index))
            {
                return;
            }

            if (Data[ParentIndex(index)].CompareTo(Data[index]) > 0)
            {
                Data.Swap(ParentIndex(index), index);

                BubbleUp(ParentIndex(index));
            }
        }

        private static bool IsRoot(int index)
        {
            return ParentIndex(index) == RootParent;
        }

        private bool IsValidIndex(int index)
        {
            return index >= 0 && index < Count;
        }

        #endregion

    }
}
