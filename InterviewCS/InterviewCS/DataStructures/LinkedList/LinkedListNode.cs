using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewCS.DataStructures.LinkedList
{
    public class LinkedListNode<T> where T : IComparable<T>
    {
        public T Data { get; set; }

        internal LinkedListNode<T> Next { get; set; }

        public LinkedListNode(T data)
        {
            Data = data;
            Next = null;
        }
    }

    public class DoublyLinkedListNode<T> where T : IComparable<T>
    {
        public T Data { get; set; }

        internal DoublyLinkedListNode<T> Next { get; set; }
        internal DoublyLinkedListNode<T> Prev { get; set; }

        public DoublyLinkedListNode(T data)
        {
            Data = data;
            Next = null;
            Prev = null;
        }
    }
}
