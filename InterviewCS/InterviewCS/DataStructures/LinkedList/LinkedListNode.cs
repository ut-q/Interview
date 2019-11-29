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
}
