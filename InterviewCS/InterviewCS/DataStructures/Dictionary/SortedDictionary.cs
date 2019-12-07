using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using InterviewCS.DataStructures.LinkedList;
using InterviewCS.DataStructures.Tree;
using InterviewCS.DataStructures.Vector;

namespace InterviewCS.DataStructures.Dictionary
{
    public class ComparableKeyValuePair<TKey, TValue> : IComparable<ComparableKeyValuePair<TKey, TValue>> where TKey : IComparable<TKey>
    { 
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public ComparableKeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public int CompareTo(ComparableKeyValuePair<TKey, TValue> other)
        {
            return Key.CompareTo(other.Key);
        }

        public override string ToString()
        {
            return Key + " " + Value;
        }
    }

    public class SortedDictionary<TKey, TValue> : IEnumerable<ComparableKeyValuePair<TKey, TValue>> where TKey : IComparable<TKey>
    {
        #region Properties and Constants

        public int Count => Tree.Count;

        public Vector<TKey> Keys { get; }

        public Vector<TKey> Values { get; }


        #endregion

        #region Constructors and Operators

        public SortedDictionary()
        {
            Tree = new BinarySearchTree<ComparableKeyValuePair<TKey, TValue>>();
        }

        public SortedDictionary(IEnumerable<ComparableKeyValuePair<TKey, TValue>> list)
        {
            Tree = new BinarySearchTree<ComparableKeyValuePair<TKey, TValue>>(list);
        }


        //TODO FIX THIS MONSTROSITY
        public TValue this[TKey key]
        {
            get
            {
                if (!Tree.Contains(new ComparableKeyValuePair<TKey, TValue>(key, default(TValue))))
                {
                    throw new KeyNotFoundException();
                }

                var pair = Tree.Find(new ComparableKeyValuePair<TKey, TValue>(key, default(TValue)));

                return pair.Value;

            }
            set
            {
                var defaultItem = new ComparableKeyValuePair<TKey, TValue>(key, value);
                if (!Tree.Contains(defaultItem))
                {
                    Tree.Insert(defaultItem);
                }
                else
                {
                    var node = Tree.Find(defaultItem);

                    node.Value = value;
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
            Tree.Clear();
        }

        public bool ContainsKey(TKey key)
        {
            var defaultItem = new ComparableKeyValuePair<TKey, TValue>(key, default(TValue));
            return Tree.Contains(defaultItem);
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

        public IEnumerator<ComparableKeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var item in Tree.ToSortedArray())
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Remove(TKey key)
        {
            Tree.Remove(new ComparableKeyValuePair<TKey, TValue>(key, default(TValue)));
        }

        public ComparableKeyValuePair<TKey, TValue>[] ToArray()
        {
            return Tree.ToSortedArray();
        }

        public override string ToString()
        {
            string s = string.Empty;
            foreach (var pair in this)
            {
                s += pair + "\n";
            }

            return s;
        }

        #endregion

        #region Private members

        private BinarySearchTree<ComparableKeyValuePair<TKey, TValue>> Tree { get; set; }

        #endregion

    }
}
