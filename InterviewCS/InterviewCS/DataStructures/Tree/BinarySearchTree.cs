using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace InterviewCS.DataStructures.Tree
{
    public class BinarySearchTree<T> : IEnumerable<T> where T: IComparable<T>
    {
        #region Properties and Constants

        public int Count { get; private set; }


        #endregion

        #region Constructors and Operators

        public BinarySearchTree()
        {
            Count = 0;
            Root = null;
        }

        public BinarySearchTree(T rootData)
        {
            Count = 0;
            Root = new TreeNode<T>(rootData);
        }

        public BinarySearchTree(IEnumerable<T> list)
        {



        }
        #endregion

        #region Functions

        public void Clear()
        {
            Root = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            return Contains_Internal(Root, item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            Queue.Queue<TreeNode<T>> q = new Queue.Queue<TreeNode<T>>();


        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Insert(T item)
        {

        }

        public void Remove(T item)
        {

        }

        public T[] ToArray()
        {

        }

        public T[] ToSortedArray()
        {

        }

        public override string ToString()
        {
            return string.Empty;
        }

        #endregion

        #region Private Members

        private TreeNode<T> Root { get; set; }

        private class TreeNode<T> : IComparable<T> where T : IComparable<T>
        {
            public TreeNode<T> Parent { get; set; }
            public TreeNode<T> Left { get; set; }
            public TreeNode<T> Right { get; set; }
            public T Data { get; set; }


            public TreeNode(T data, TreeNode<T> parent = null)
            {
                Parent = parent;
                Left = null;
                Right = null;
                Data = data;
            }

            public int CompareTo(TreeNode<T> obj)
            {
                return Data.CompareTo(obj.Data);
            }
        }

        #endregion

        private bool Contains_Internal(TreeNode<T> node, T item)
        {
            if (node == null)
            {
                return false;
            }

            if (node.Data.CompareTo(item) == 0)
            {
                return true;
            }

            if (node.Data.CompareTo(item) < 0)
            {
                return Contains_Internal(node.Left, item);
            }
            else
            {
                return Contains_Internal(node.Right, item);
            }
        }

    }
}
