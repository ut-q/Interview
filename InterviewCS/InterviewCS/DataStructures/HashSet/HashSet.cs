using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterviewCS.DataStructures.LinkedList;
using InterviewCS.DataStructures.Vector;

namespace InterviewCS.DataStructures.HashSet
{
    public class HashSet<T> : IEnumerable<T>
    {
        #region Properties and Constants

        public int Count { get; private set; }
        private const double _maxLoadFactor = 0.8;

        public double MaxLoadFactor { get; }


        private const int _defaultCapacity = 17;

        public double LoadFactor => Items.Capacity > 0 ? (double)Count / Items.Capacity : 0;

        #endregion

        #region Constructors and Operators

        public HashSet(double loadFactor = _maxLoadFactor)
        {
            Count = 0;
            MaxLoadFactor = loadFactor;
            Items = new Vector<DoublyLinkedList<T>>(_defaultCapacity);
            InitItems();
        }

        public HashSet(int capacity, double loadFactor = _maxLoadFactor)
        {
            Count = 0;
            MaxLoadFactor = loadFactor;
            Items = new Vector<DoublyLinkedList<T>>(capacity);
            InitItems();
        }

        public HashSet(IEnumerable<T> list, int capacity, double loadFactor = _maxLoadFactor)
        {
            Count = 0;
            MaxLoadFactor = loadFactor;
            Items = new Vector<DoublyLinkedList<T>>(capacity);
            InitItems();

            foreach (var val in list)
            {
                Add(val);
            }
        }

        #endregion

        #region Functions

        public bool Add(T item)
        {
            if (Contains(item))
            {
                return false;
            }

            int index = GetPosition(item);

            Items[index].AddFirst(item);

            Count++;

            if (RequiresReallocation())
            {
                Reallocate();
            }

            return true;
        }

        public void Clear()
        {
            Items.Clear();
            Count = 0;
        }

        public bool Contains(T item)
        {
            int index = GetPosition(item);
            return Items[index].Exists((p) => p.Equals(item));
        }

        public void ExceptWith(IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var val in list)
            {
                Remove(val);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var list in Items)
            {
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        yield return item.Data;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void IntersectWith(IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var val in list)
            {
                Add(val);
            }
        }

        public bool IsProperSubsetOf(IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException();
            }

            var enumerable = list as T[] ?? list.ToArray();
            if (Count == 0 && enumerable.Count() != 0)
            {
                return true;
            }

            if (Count >= enumerable.Count())
            {
                return false;
            }

            return Internal_IsSubsetOf(enumerable);
        }

        public bool IsSubsetOf(IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException();
            }

            var enumerable = list as T[] ?? list.ToArray();
            if (Count == 0 && enumerable.Count() != 0)
            {
                return true;
            }

            if (Count > enumerable.Count())
            {
                return false;
            }

            return Internal_IsSubsetOf(enumerable);
        }

        public bool IsProperSupersetOf(IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException();
            }

            var enumerable = list as T[] ?? list.ToArray();
            if (Count != 0 && !enumerable.Any())
            {
                return true;
            }

            if (Count <= enumerable.Count())
            {
                return false;
            }

            return Internal_IsSuperSetOf(enumerable);
        }

        public bool IsSupersetOf(IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException();
            }

            var enumerable = list as T[] ?? list.ToArray();
            if (Count != 0 && !enumerable.Any())
            {
                return true;
            }

            if (Count < enumerable.Count())
            {
                return false;
            }

            return Internal_IsSuperSetOf(enumerable);
        }

        public bool Overlaps(IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var item in list)
            {
                if (Contains(item))
                {
                    return true;
                }
            }

            return false;
        }

        public void Remove(T item)
        {
            int index = GetPosition(item);

            if (Items[index].Exists((p) => p.Equals(item)))
            {
                Items[index].Remove(Items[index].Find((p) => p.Equals(item)));

                Count--;
            }
        }

        public void RemoveWhere(Predicate<T> predicate)
        {
            foreach (var list in Items)
            {
                if (list.Exists(predicate))
                {
                    list.Remove(list.Find(predicate));

                    Count--;
                }
            }

        }

        public void SymmetricExceptWith(IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var val in list)
            {
                if (Contains(val))
                {
                    Remove(val);
                }
                else
                {
                    Add(val);
                }
            }
        }

        public void UnionWith(IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var val in list)
            {
                Add(val);
            }
        }

        public T[] ToArray()
        {
            var arr = new T[Count];

            int i = 0;
            foreach (var item in this)
            {
                arr[i] = item;
                ++i;
            }

            return arr;
        }

        public override string ToString()
        {
            string s = string.Empty;
            foreach (var item in this)
            {
                s += item + " ";
            }

            return s;
        }

        #endregion

        #region Private Members

        private Vector<DoublyLinkedList<T>> Items { get; set; }

        private int GetPosition(T item)
        {
            return Math.Abs(item.GetHashCode()) % Items.Capacity;
        }

        private bool RequiresReallocation()
        {
            return LoadFactor >= MaxLoadFactor;
        }

        private void Reallocate()
        {
            var oldItems = ToArray();
            Clear();

            Items = new Vector<DoublyLinkedList<T>>(GetNewCapacity(Items.Capacity));
            InitItems();

            foreach (var item in oldItems)
            {
                Add(item);
            }
        }

        private int GetNewCapacity(int capacity)
        {
            return capacity * 2;
        }

        private void InitItems()
        {
            for (int i = 0; i < Items.Capacity; ++i)
            {
                Items.Add(new DoublyLinkedList<T>());
            }
        }

        private bool Internal_IsSubsetOf(IEnumerable<T> list)
        {
            foreach (var i in this)
            {
                if (!list.Contains(i))
                {
                    return false;
                }
            }

            return true;
        }

        private bool Internal_IsSuperSetOf(IEnumerable<T> list)
        {
            foreach (var i in list)
            {
                if (!Contains(i))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
