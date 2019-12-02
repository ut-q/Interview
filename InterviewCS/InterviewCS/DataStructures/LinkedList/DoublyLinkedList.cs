using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace InterviewCS.DataStructures.LinkedList
{
    public class DoublyLinkedList<T> : IEnumerable<DoublyLinkedListNode<T>> where T : IComparable<T>
    {
        #region Properties

        public int Count { get; protected set; }

        public DoublyLinkedListNode<T> Head { get; protected set; }

        public DoublyLinkedListNode<T> Tail { get; protected set; }

        #endregion

        #region Constructors

        public DoublyLinkedList()
        {
            Count = 0;
            Head = null;
            Tail = null;
        }

        public DoublyLinkedList(IEnumerable<T> list)
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
        public virtual void AddAfter(DoublyLinkedListNode<T> node, T data)
        {
            if (node == null)
            {
                throw new Exception("null node pass for Add");
            }

            var next = node.Next;

            var n = new DoublyLinkedListNode<T>(data);

            node.Next = n;
            n.Prev = node;

            n.Next = next;

            if (next != null)
            {
                next.Prev = n;
            }

            Count++;

            // make sure Tail is cached properly
            if (node == Tail)
            {
                Tail = n;
            }
        }

        public virtual void AddBefore(DoublyLinkedListNode<T> node, T data)
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

            var newNode = new DoublyLinkedListNode<T>(data);

            node.Prev.Next = newNode;
            newNode.Next = node;
            newNode.Prev = node.Prev;
            node.Prev = newNode;

            Count++;
        }

        public virtual void AddFirst(T data)
        {
            var n = new DoublyLinkedListNode<T>(data) { Next = Head };

            if (Head != null)
            {
                Head.Prev = n;
            }
            Head = n;

            if (Count == 0)
            {
                Tail = Head;
            }

            Count++;
        }

        public void AddLast(T data)
        {
            var n = new DoublyLinkedListNode<T>(data);

            if (Tail != null)
            {
                Tail.Next = n;
            }

            n.Prev = Tail;
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

        public DoublyLinkedListNode<T> Find(T data)
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

        public IEnumerator<DoublyLinkedListNode<T>> GetEnumerator()
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

            Tail = Tail.Prev;
            Tail.Next = null;

            if (Count == 1)
            {
                Tail = Head;
            }
            Count--;
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
