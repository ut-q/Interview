using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using InterviewCS.DataStructures.Vector;

namespace InterviewCS.DataStructures.LinkedList
{
    public class LinkedList<T> : IEnumerable<LinkedListNode<T>>
    {
        #region Properties

        public int Count { get; private set; }

        public LinkedListNode<T> Head { get; private set; }

        public LinkedListNode<T> Tail { get; private set; }

        #endregion

        #region Constructors

        public LinkedList()
        {
            Count = 0;
            Head = null;
            Tail = null;
        }

        public LinkedList(IEnumerable<T> list)
        {
            Count = 0;
            Head = null;
            Tail = null;

            foreach (var val in list)
            {
                //add each to the list
                //should add backwards but no time to do all this perfect
                this.AddLast(val);
            }

        }

        #endregion

        #region Functions

        // O(1), adds after the given node
        public void AddAfter(LinkedListNode<T> node, T data)
        {
            if (node == null)
            {
                throw new Exception("null node pass for Add");
            }

            var next = node.Next;

            var n = new LinkedListNode<T>(data);

            node.Next = n;

            n.Next = next;

            Count++;

            // make sure Tail is cached properly
            if (node == Tail)
            {
                Tail = n;
            }
        }

        public void AddBefore(LinkedListNode<T> node, T data)
        {
            if (node == null)
            {
                return;
            }

            if (node == Head)
            {
                AddFirst(data);
                return;
            }

            foreach (var n in this)
            {
                if (n.Next != null && n.Next == node)
                {
                    var newNode = new LinkedListNode<T>(data) { Next = n.Next};
                    n.Next = newNode;

                    Count++;

                    return;
                }
            }
        }

        public void AddFirst(T data)
        {
            var n = new LinkedListNode<T>(data) {Next = Head};

            Head = n;

            if (Count == 0)
            {
                Tail = Head;
            }

            Count++;
        }

        public void AddLast(T data)
        {
            var n = new LinkedListNode<T>(data);

            if (Tail != null)
            {
                Tail.Next = n;
            }

            Tail = n;

            if (Count == 0)
            {
                Head = Tail;
            }

            Count++;
        }

        public void Clear()
        {
            Count = 0;
            Tail = Head = null;
        }

        public bool Contains(T data)
        {
            foreach (var node in this)
            {
                if (node.Data.Equals(data))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Exists(Predicate<T> predicate)
        {
            foreach (var i in this)
            {
                if (predicate(i.Data))
                {
                    return true;
                }
            }

            return false;
        }

        public LinkedListNode<T> Find(T data)
        {
            foreach (var node in this)
            {
                if (node.Data.Equals(data))
                {
                    return node;
                }
            }

            return null;
        }

        public T Find(Predicate<T> predicate)
        {
            foreach (var i in this)
            {
                if (predicate(i.Data))
                {
                    return i.Data;
                }
            }

            return default(T);
        }

        public Vector<T> FindAll(Predicate<T> predicate)
        {
            var vector = new Vector<T>();

            foreach (var i in this)
            {
                if (predicate(i.Data))
                {
                    vector.Add(i.Data);
                }
            }

            return vector;
        }

        public IEnumerator<LinkedListNode<T>> GetEnumerator()
        {
            var node = Head;

            while (node != null)
            {
                yield return node;
                node = node.Next;
            }
        }

        public void Remove(T data)
        {
            //if removing head
            if (Head != null && Head.Data.Equals(data))
            {
                RemoveFirst();
                return;
            }

            //if removing tail
            if (Tail != null && Tail.Data.Equals(data))
            {
                RemoveLast();
                return;
            }

            foreach (var node in this)
            {
                if (node.Next != null && node.Next.Data.Equals(data))
                {
                    node.Next = node.Next.Next;

                    if (node.Next == Tail)
                    {
                        Tail = node;
                    }

                    Count--;

                    return;
                }
            }
        }

        public void RemoveFirst()
        {
            if (Head == null)
            {
                return;
            }

            Head = Head.Next;
            if (Count == 1)
            {
                Tail = Head;
            }

            Count--;
        }

        public void RemoveLast()
        {
            if (Count <= 1)
            {
                RemoveFirst();
                return;
            }

            foreach (var node in this)
            {
                if (node != null && node.Next != null)
                {
                    if (node.Next == Tail)
                    {
                        Tail = node;
                        Tail.Next = null;

                        Count--;

                        return;
                    }
                }
            }
        }

        public override string ToString()
        {
            var s = string.Empty;

            foreach (var node in this)
            {
                s += node.Data + " -> ";
            }

            return s;
        }

        #endregion

        #region Private Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion


    }
}
