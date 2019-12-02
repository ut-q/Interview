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
            foreach (var item in list)
            {
                Insert(item);
            }
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

            if (Count > 0)
            {
                q.Enqueue(Root);
            }

            while (q.Count != 0)
            {
                var node = q.Dequeue();

                yield return node.Data;

                if (node.Left != null)
                {
                    q.Enqueue(node.Left);
                }

                if (node.Right != null)
                {
                    q.Enqueue(node.Right);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Insert(T item)
        {
            TreeNode<T> parent = null;
            TreeNode<T> node = Root;

            TreeNode<T> newNode = new TreeNode<T>(item);

            while (node != null)
            {
                parent = node;
                node = node.CompareTo(newNode) > 0 ? node.Left : node.Right;
            }

            newNode.Parent = parent;

            if (parent == null)
            {
                Root = newNode;
            }
            else if (newNode.CompareTo(parent) < 0)
            {
                parent.Left = newNode;
            }
            else
            {
                parent.Right = newNode;
            }

            Count++;
        }

        public T Maximum()
        {
            return MaximumNode().Data;
        }

        public T Minimum()
        {
            return MinimumNode().Data;
        }

        public void Remove(T item)
        {
            var node = Find(Root, item);
            if (node == null)
            {
                return;
            }

            if (node.Left == null)
            {
                Transplant(node, node.Right);
            }
            else if (node.Right == null)
            {
                Transplant(node, node.Left);
            }
            else
            {
                var replacement = MinimumNode(node.Right);
                if (replacement.Parent != node)
                {
                    Transplant(replacement, replacement.Right);
                    replacement.Right = node.Right;
                    replacement.Right.Parent = replacement;
                }

                Transplant(node, replacement);
                replacement.Left = node.Left;
                replacement.Left.Parent = replacement;
            }

            Count--;
        }

        // To array in breadth first search
        public T[] ToArray()
        {
            T[] arr = new T[Count];

            int i = 0;
            foreach (var t in this)
            {
                arr[i] = t;
                ++i;
            }

            return arr;
        }

        // sorted array by inorder DFS
        public T[] ToSortedArray()
        {
            T[] arr = new T[Count];

            int i = 0;

            var stack = new Stack.Stack<TreeNode<T>>();

            var node = Root;

            while (node != null || stack.Count != 0)
            {
                while (node != null)
                {
                    stack.Push(node);

                    node = node.Left;
                }

                node = stack.Pop();
                arr[i] = node.Data;
                ++i;

                node = node.Right;
            }

            return arr;
        }

        public override string ToString()
        {
            string s = string.Empty;

            foreach (var i in this)
            {
                s += i + " ";
            }

            return s;
        }

        #endregion

        #region Private Members

        private TreeNode<T> Root { get; set; }

        private class TreeNode<T> : IComparable<TreeNode<T>> where T : IComparable<T>
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

            public bool IsRoot()
            {
                return Parent == null;
            }

            public bool IsLeftChild()
            {
                return Parent.Left != null && CompareTo(Parent.Left) == 0;
            }

            public bool IsRightChild()
            {
                return Parent.Right != null && CompareTo(Parent.Right) == 0;
            }
        }

        #endregion

        private void Transplant(TreeNode<T> source, TreeNode<T> dest)
        {
            if (source.IsRoot())
            {
                Root = dest;
            }

            if (source.IsLeftChild())
            {
                source.Parent.Left = dest;
            }
            else if (source.IsRightChild())
            {
                source.Parent.Right = dest;
            }

            if (dest != null)
            {
                dest.Parent = source.Parent;
            }
        }

        private TreeNode<T> Find(TreeNode<T>  node, T item)
        {
            if (node == null)
            {
                return null;
            }

            if (node.Data.CompareTo(item) == 0)
            {
                return node;
            }

            if (node.Data.CompareTo(item) < 0)
            {
                return Find(node.Right, item);
            }
            else
            {
                return Find(node.Left, item);
            }
        }

        private TreeNode<T> MaximumNode(TreeNode<T> node = null)
        {
            if (Root == null)
            {
                return null;
            }

            if (node == null)
            {
                node = Root;
            }

            while (node.Right != null)
            {
                node = node.Right;
            }

            return node;
        }

        private TreeNode<T> MinimumNode(TreeNode<T> node = null)
        {
            if (Root == null)
            {
                return null;
            }

            if (node == null)
            {
                node = Root;
            }

            while (node.Left != null)
            {
                node = node.Left;
            }

            return node;
        }

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

            if (node.Data.CompareTo(item) > 0)
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
