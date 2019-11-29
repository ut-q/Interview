using System;
using System.Collections;
using System.Collections.Generic;
using InterviewCS.DataStructures.Vector;
using System.Text;

namespace InterviewCS.DataStructures.Stack
{
    public class Stack<T> : IEnumerable<T> where T : IEquatable<T>
    {
        #region Properties and Constants

        public int Count => vector.Count;

        #endregion

        #region Constructors and Operators

        public Stack()
        {
            vector = new Vector<T>();
        }

        public Stack(IEnumerable<T> list)
        {
            vector = new Vector<T>(list);
        }

        public Stack(int capacity)
        {
            vector = new Vector<T>(capacity);
        }

        #endregion

        #region Functions

        public void Clear()
        {
            vector.Clear();
        }

        public bool Contains(T item)
        {
            return vector.Contains(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return vector.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T Peek()
        {
            return Count > 0 ? vector[Count - 1] : default(T);
        }

        public T Pop()
        {
            var top = Peek();
            vector.RemoveAt(Count -1);

            return top;
        }

        public void Push(T item)
        {
            vector.Add(item);
        }

        public T[] ToArray()
        {
            return vector.ToArray();
        }

        public void TrimExcess()
        {
            vector.TrimExcess();
        }

        public override string ToString()
        {
            return vector.ToString();
        }

        #endregion

        #region Private Members

        private readonly Vector<T> vector;

        #endregion

    }
}
