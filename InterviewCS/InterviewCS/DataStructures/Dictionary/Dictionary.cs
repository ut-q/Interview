using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using InterviewCS.DataStructures.LinkedList;
using InterviewCS.DataStructures.Vector;

namespace InterviewCS.DataStructures.Dictionary
{
    public class Dictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey,TValue>>
    {
        #region Properties and Constants

        public int Count { get; private set; }

        public Vector<TKey> Keys { get; }

        public Vector<TKey> Values { get; }

        private const double _maxLoadFactor = 0.8;

        public double MaxLoadFactor { get; }


        private const int _defaultCapacity = 8;

        public double LoadFactor => Items.Capacity > 0 ? (double)Count / Items.Capacity : 0;

        #endregion

        #region Constructors and Operators

        public Dictionary(double loadFactor = _maxLoadFactor)
        {
            Count = 0;
            MaxLoadFactor = loadFactor;
            Items = new Vector<DoublyLinkedList<KeyValuePair<TKey, TValue>>>(_defaultCapacity);
            InitItems();
        }

        public Dictionary(int capacity, double loadFactor = _maxLoadFactor)
        {
            Count = 0;
            MaxLoadFactor = loadFactor;
            Items = new Vector<DoublyLinkedList<KeyValuePair<TKey, TValue>>>(capacity);
            InitItems();
        }

        public Dictionary(IEnumerable<KeyValuePair<TKey,TValue>> list, int capacity, double loadFactor = _maxLoadFactor)
        {
            Count = 0;
            MaxLoadFactor = loadFactor;
            Items = new Vector<DoublyLinkedList<KeyValuePair<TKey, TValue>>>(capacity);
            InitItems();

            foreach (var val in list)
            {
                Add(val.Key, val.Value);
            }
        }


        // TODO optimize this
        public TValue this[TKey key]
        {
            get
            {
                int index = GetPosition(key);
                var list = Items[index];
                if (!list.Exists((p) => p.Key.Equals(key)))
                {
                    throw new KeyNotFoundException();
                }

                var pair = list.Find((p) => p.Key.Equals(key));

                return pair.Value;

            }
            set
            {
                // remove if exists
                Remove(key);

                int index = GetPosition(key);

                Items[index].AddFirst(new KeyValuePair<TKey, TValue>(key,value));

                Count++;

                if (RequiresReallocation())
                {
                    Reallocate();
                }
            }
        }

        #endregion

        #region Functions

        public void Add(TKey key, TValue val)
        {
            if (ContainsKey(key))
            {
                throw new ArgumentException();
            }

            this[key] = val;
        }

        public void Clear()
        {
            Items.Clear();
            Count = 0;
        }

        public bool ContainsKey(TKey key)
        {
            int index = GetPosition(key);
            return Items[index].Exists((p) => p.Key.Equals(key));
        }

        public bool ContainsValue(TValue val)
        {
            foreach (var pair in this)
            {
                if (pair.Value.Equals(val))
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var list in Items)
            {
                if (list != null)
                {
                    foreach (var pair in list)
                    {
                        yield return pair.Data;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Remove(TKey key)
        {
            int index = GetPosition(key);

            if (Items[index].Exists((p) => p.Key.Equals(key)))
            {
                Items[index].Remove(Items[index].Find((p) => p.Key.Equals(key)));

                Count--;
            }
        }

        public KeyValuePair<TKey, TValue>[] ToArray()
        {
            var arr = new KeyValuePair<TKey,TValue>[Count];

            int i = 0;
            foreach (var pair in this)
            {
                arr[i] = pair;
                ++i;
            }

            return arr;
        }

        public override string ToString()
        {
            string s = string.Empty;
            foreach (var pair in this)
            {
                s += "Key: " + pair.Key + " Pair: " + pair.Value + "\n";
            }

            return s;
        }

        #endregion

        #region Private members

        private Vector<DoublyLinkedList <KeyValuePair<TKey, TValue>> > Items { get; set; }

        private int GetPosition(TKey key)
        {
            return key.GetHashCode() % Items.Capacity;
        }

        private bool RequiresReallocation()
        {
            return LoadFactor >= MaxLoadFactor;
        }

        private void Reallocate()
        {
            var oldItems = ToArray();
            Clear();

            Items = new Vector<DoublyLinkedList<KeyValuePair<TKey, TValue>>>(GetNewCapacity(Items.Capacity));
            InitItems();

            foreach (var pair in oldItems)
            {
                Add(pair.Key, pair.Value);
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
                Items.Add(new DoublyLinkedList<KeyValuePair<TKey, TValue>>());
            }
        }

        #endregion

    }
}
