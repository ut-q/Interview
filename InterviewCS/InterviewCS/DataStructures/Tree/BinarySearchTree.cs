using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace InterviewCS.DataStructures.Tree
{
    public class BinarySearchTree<T> : IEnumerable<T> where T: IComparable<T>
    {
        #region Properties and Constants

        public int Count { get; protected set; }


        #endregion

        #region Constructors and Operators

        public BinarySearchTree()
        {
            Count = 0;
            Root = null;
            EdgeSentinel = null;
        }

        public BinarySearchTree(T rootData)
        {
            Count = 0;
            Root = new TreeNode<T>(rootData);
            EdgeSentinel = null;
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

        //TODO FIX THIS MONSTROSITY
        public T Find(T item)
        {
            var node = Find_Internal(Root, item);
            return node != null ? node.Data : default(T);
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

                if (!IsEdgeSentinel(node.Left))
                {
                    q.Enqueue(node.Left);
                }

                if (!IsEdgeSentinel(node.Right))
                {
                    q.Enqueue(node.Right);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // TODO implement identical keys
        public virtual void Insert(T item)
        {
            TreeNode<T> node = new TreeNode<T>(item, edgeSentinel: EdgeSentinel);
            Insert_Internal(node);
        }

        public T Maximum()
        {
            return MaximumNode().Data;
        }

        public T Minimum()
        {
            return MinimumNode().Data;
        }

        public virtual void Remove(T item)
        {
            var node = Find_Internal(Root, item);
            if (node == null)
            {
                return;
            }

            if (IsEdgeSentinel(node.Left))
            {
                Transplant(node, node.Right);
            }
            else if (IsEdgeSentinel(node.Right))
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
                while (node != null && !IsEdgeSentinel(node))
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

        protected TreeNode<T> Root { get; set; }

        protected TreeNode<T> EdgeSentinel { get; set; }

        protected class TreeNode<T> : IComparable<TreeNode<T>> where T : IComparable<T>
        {
            public TreeNode<T> Parent { get; set; }
            public TreeNode<T> Left { get; set; }
            public TreeNode<T> Right { get; set; }
            public T Data { get; set; }

            // reference for the children of edge nodes
            private TreeNode<T> EdgeSentinel { get; set; }

            // don't use this
            public TreeNode()
            {
                EdgeSentinel = null;
                Parent = null;
                Left = EdgeSentinel;
                Right = EdgeSentinel;
                Data = default(T);
            }

            public TreeNode(T data, TreeNode<T> parent = null, TreeNode<T> edgeSentinel = null)
            {
                Parent = parent;
                EdgeSentinel = edgeSentinel;
                Left = EdgeSentinel;
                Right = EdgeSentinel;
                Data = data;
            }

            public int CompareTo(TreeNode<T> obj)
            {
                if (obj == null)
                {
                    return 1;
                }
                if (Data == null && obj.Data == null)
                {
                    return 0;
                }
                if (Data == null)
                {
                    return -1;
                }
                if (obj.Data == null)
                {
                    return 1;
                }
                return Data.CompareTo(obj.Data);
            }

            public bool IsLeftChild()
            {
                return Parent.Left != null && this == Parent.Left;
            }

            public bool IsRightChild()
            {
                return Parent.Right != null && this == Parent.Right;
            }
        }

        #endregion

        protected void Transplant(TreeNode<T> source, TreeNode<T> dest)
        {
            if (IsRoot(source))
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

        protected TreeNode<T> Find_Internal(TreeNode<T>  node, T item)
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
                return Find_Internal(node.Right, item);
            }
            else
            {
                return Find_Internal(node.Left, item);
            }
        }

        protected TreeNode<T> MaximumNode(TreeNode<T> node = null)
        {
            if (Root == null)
            {
                return null;
            }

            if (node == null)
            {
                node = Root;
            }

            while (!IsEdgeSentinel(node.Right))
            {
                node = node.Right;
            }

            return node;
        }

        protected TreeNode<T> MinimumNode(TreeNode<T> node = null)
        {
            if (Root == null)
            {
                return null;
            }

            if (node == null)
            {
                node = Root;
            }

            while (!IsEdgeSentinel(node.Left))
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

        protected bool IsEdgeSentinel(TreeNode<T> node)
        {
            return node == null || node == EdgeSentinel;
        }

        protected virtual bool IsRoot(TreeNode<T> node)
        {
            return node != null && node.Parent == null;
        }

        protected void Insert_Internal(TreeNode<T> newNode)
        {
            TreeNode<T> parent = null;
            TreeNode<T> node = Root;

            while (!IsEdgeSentinel(node))
            {
                parent = node;
                node = node.CompareTo(newNode) > 0 ? node.Left : node.Right;
            }

            newNode.Parent = parent;

            if (IsRoot(newNode))
            {
                Root = newNode;
            }
            else if (parent != null && newNode.CompareTo(parent) < 0)
            {
                parent.Left = newNode;
            }
            else if(parent != null)
            {
                parent.Right = newNode;
            }

            Count++;
        }

    }
}
