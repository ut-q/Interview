using System;
using System.Collections;
using System.Collections.Generic;

//TODO implement this with circular vector

namespace InterviewCS.DataStructures.Queue
{
    public class Queue<T> : IEnumerable<T>
    {
        #region Properties and Constants

        public int Count => list.Count;

        #endregion

        #region Constructors and Operators

        public Queue()
        {
            list = new InterviewCS.DataStructures.LinkedList.LinkedList<T>();
        }

        public Queue(IEnumerable<T> items)
        {
            list = new InterviewCS.DataStructures.LinkedList.LinkedList<T>(items);
        }

        #endregion

        #region Functions

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public T Dequeue()
        {
            var front = Peek();
            list.RemoveFirst();

            return front;
        }

        public void Enqueue(T item)
        {
            list.AddLast(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var node in list)
            {
                yield return node.Data;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T Peek()
        {
            return Count > 0 ? list.Head.Data : default(T);
        }

        public override string ToString()
        {
            return list.ToString();
        }

        #endregion

        #region Private Members

        private readonly InterviewCS.DataStructures.LinkedList.LinkedList<T> list;

        #endregion
    }
}
