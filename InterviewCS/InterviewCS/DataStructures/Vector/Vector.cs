using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewCS.DataStructures.Vector
{
    public class Vector<T> : IEnumerable<T> where T:IEquatable<T>
    {
        #region Properties and Constants

        private const int DefaultCapacity = 0;
        private const float TrimThreshold = 0.9f;

        public int Capacity { get; private set; }

        public int Count { get; private set; }

        #endregion

        #region Constructors and Operators

        public Vector()
        {
            Capacity = DefaultCapacity;
            Count = 0;
            Data = null;
        }

        public Vector(IEnumerable<T> data)
        {
            Capacity = DefaultCapacity;
            Count = 0;

            foreach (var d in data)
            {
                Add(d);
            }
        }

        public Vector(int capacity)
        {
            Capacity = capacity;
            Count = 0;
            Data = new T[Capacity];
        }

        public T this[int index]
        {
            get => Data[index];
            set => Data[index] = value;
        }

        #endregion

        #region Functions

        public void Add(T item)
        {
            if (RequiresReallocation())
            {
                ReallocateVector();
            }

            Count++;
            Data[Count - 1] = item;
        }

        public int BinarySearch(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            Count = 0;
        }

        public bool Contains(T item)
        {
            foreach (var i in this)
            {
                if (i.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public T Find(Predicate<T> predicate)
        {
            foreach (var i in this)
            {
                if (predicate(i))
                {
                    return i;
                }
            }

            return default(T);
        }

        public Vector<T> FindAll(Predicate<T> predicate)
        {
            var vector = new Vector<T>();

            foreach (var i in this)
            {
                if (predicate(i))
                {
                   vector.Add(i);
                }
            }

            return vector;
        }

        public int FindIndex(Predicate<T> predicate)
        {
            for (int i = 0; i < Count; ++i)
            {
                if (predicate(Data[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public void ForEach(Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException();
            }
            foreach (var i in this)
            {
                action.Invoke(i);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for(int i = 0; i < Count; ++i)
            {
                yield return Data[i];
            }
        }

        public int IndexOf(T item)
        {
            return FindIndex(x => x.Equals(item));
        }

        public void Insert(int index, T item)
        {
            if (RequiresReallocation())
            {
                ReallocateVector();
            }

            for (int i = Count - 1; i >= index; --i)
            {
                Data[i + 1] = Data[i];
            }

            Data[index] = item;

            Count++;
        }

        public void Remove(T item)
        {
            int index = IndexOf(item);

            RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            for (int i = index; i < Count - 1; ++i)
            {
                Data[i] = Data[i + 1];
            }

            Count = Math.Max(0, Count - 1);
        }

        public void Reverse()
        {
            var reverse = new T[Capacity];

            for (int i = 0; i < Count; ++i)
            {
                reverse[Count - 1 - i] = Data[i];
            }

            Data = reverse;
        }

        public void Sort()
        {
            throw new NotImplementedException();
        }

        public T[] ToArray()
        {
            var copy = new T[Count];
            for (int i = 0; i < Count; i++)
            {
                copy[i] = Data[i];
            }

            return copy;
        }

        public override string ToString()
        {
            string s = string.Empty;

            foreach (var i in this)
            {
                s += i + " ";
            }

            return s;
        }

        public void TrimExcess()
        {
            if (ShouldTrimExcess())
            {
                Data = ToArray();
                Capacity = Count;
            }
        }

        #endregion

        #region Private Members

        private T[] Data { get; set; }

        private void ReallocateVector()
        {
            Capacity = NewCapacity();

            var newData = new T[Capacity];

            for (var i = 0; i < Count; ++i)
            {
                newData[i] = Data[i];
            }

            Data = newData;
        }

        private int NewCapacity()
        {
            return Capacity == 0 ? 2 : Capacity * 2;
        }

        private bool RequiresReallocation()
        {
            return Capacity <= Count;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private bool ShouldTrimExcess()
        {
            return ((float) Count / Capacity) < TrimThreshold;
        }

        #endregion
    }
}
