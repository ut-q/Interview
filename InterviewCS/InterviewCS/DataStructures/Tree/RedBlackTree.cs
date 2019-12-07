using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace InterviewCS.DataStructures.Tree
{
    public class RedBlackTree<T> : BinarySearchTree<T> where T : IComparable<T>
    {
        #region Properties and Constants

        #endregion

        #region Constructors and Operators

        public RedBlackTree()
        {
            Count = 0;
            EdgeSentinel = new RBTreeNode<T>(default(T), Color.Black);
            Root = null;
        }

        public RedBlackTree(T rootData)
        {
            Count = 0;
            EdgeSentinel = new RBTreeNode<T>(default(T), Color.Black);
            Root = new RBTreeNode<T>(rootData, Color.Black, EdgeSentinel);
        }

        public RedBlackTree(IEnumerable<T> list) : base(list)
        {

        }
        #endregion

        #region Functions

        public override void Insert(T item)
        {
            RBTreeNode<T> node = new RBTreeNode<T>(item, edgeSentinel: EdgeSentinel);
            Insert_Internal(node);
            node.Color = Color.Red;

            InsertFixUp(node);
        }

        public override void Remove(T item)
        {
            var node = Find_Internal(Root, item);
            if (node == null)
            {
                return;
            }

            var y = (RBTreeNode<T>)node;
            var originalYColor = y.Color;
            RBTreeNode<T> x;

            if (IsEdgeSentinel(node.Left))
            {
                x = (RBTreeNode<T>)node.Right;
                Transplant(node, node.Right);
            }
            else if (IsEdgeSentinel(node.Right))
            {
                x = (RBTreeNode<T>)node.Left;
                Transplant(node, node.Left);
            }
            else
            {
                y = (RBTreeNode<T>)MinimumNode(node.Right);
                originalYColor = y.Color;
                x = y.RBRight;
                if (y.Parent == node)
                {
                    x.Parent = node;
                }
                else
                {
                    Transplant(y, y.Right);
                    y.Right = node.Right;
                    y.Right.Parent = y;
                }

                Transplant(node, y);
                y.Left = node.Left;
                y.Left.Parent = y;
                y.Color = ((RBTreeNode<T>) node).Color;
            }

            if (originalYColor == Color.Black)
            {
                RemoveFixup(x);
            }

            Count--;

        }

        #endregion

        #region Private Members

        private RBTreeNode<T> RBRoot => (RBTreeNode<T>)Root;

        private enum Color
        {
            Black,
            Red
        };

        private class RBTreeNode<T> : TreeNode<T> where T : IComparable<T>
        {
            public Color Color { get; set; }

            public RBTreeNode<T> RBParent => (RBTreeNode<T>)Parent;

            public RBTreeNode<T> RBLeft => (RBTreeNode<T>)Left;
            public RBTreeNode<T> RBRight => (RBTreeNode<T>)Right;

            public RBTreeNode() : base()
            {
                Color = Color.Black;
            }

            public RBTreeNode(T data, Color color = Color.Black, TreeNode<T> parent = null, TreeNode<T> edgeSentinel = null) : base(data, parent, edgeSentinel)
            {
                Color = color;
            }


        }

        private void RotateLeft(RBTreeNode<T> node)
        {
            if (node == null)
            {
                throw new ArgumentException();
            }

            var newParent = node.Right;
            node.Left = newParent.Left;
            if (!IsEdgeSentinel(newParent.Left))
            {
                node.Left.Parent = node;
            }

            newParent.Parent = node.Parent;

            if (IsRoot(node))
            {
                Root = newParent;
            }
            else if (node.Parent.Left == node)
            {
                node.Parent.Left = newParent;
            }
            else if (node.Parent.Right == node)
            {
                node.Parent.Right = newParent;
            }

            newParent.Left = node;
            node.Parent = newParent;
        }

        private void RotateRight(RBTreeNode<T> node)
        {
            if (node == null)
            {
                throw new ArgumentException();
            }

            var newParent = node.Left;
            newParent.Right = node.Left;
            if (!IsEdgeSentinel(node.Left))
            {
                node.Left.Parent = node;
            }

            newParent.Parent = node.Parent;

            if (IsRoot(node))
            {
                Root = newParent;
            }
            else if (node.Parent.Left == node)
            {
                node.Parent.Left = newParent;
            }
            else if (node.Parent.Right == node)
            {
                node.Parent.Right = newParent;
            }

            newParent.Right = node;
            node.Parent = newParent;
        }

        protected override bool IsRoot(TreeNode<T> node)
        {
            return node != null && IsEdgeSentinel(node.Parent);
        }

        private void InsertFixUp(RBTreeNode<T> z)
        {
            if (z == null || IsRoot(z) || IsRoot(z.Parent))
            {
                return;
            }

            while (z.RBParent.Color == Color.Red)
            {
                if (z.RBParent == z.RBParent.RBParent.RBLeft)
                {
                    var y = z.RBParent.RBParent.RBRight;
                    if (y.Color == Color.Red)
                    {
                        z.RBParent.Color = Color.Black;
                        y.Color = Color.Black;
                        z.RBParent.RBParent.Color = Color.Red;
                        z = z.RBParent.RBParent;
                    }
                    else if (z == z.RBParent.RBRight)
                    {
                        z = z.RBParent;
                        RotateLeft(z);
                    }

                    z.RBParent.Color = Color.Black;
                    z.RBParent.RBParent.Color = Color.Red;
                    RotateRight(z.RBParent.RBParent);
                }
                else
                {
                    var y = z.RBParent.RBParent.RBLeft;
                    if (y.Color == Color.Red)
                    {
                        z.RBParent.Color = Color.Black;
                        y.Color = Color.Black;
                        z.RBParent.RBParent.Color = Color.Red;
                        z = z.RBParent.RBParent;
                    }
                    else if (z == z.RBParent.RBLeft)
                    {
                        z = z.RBParent;
                        RotateRight(z);
                    }

                    z.RBParent.Color = Color.Black;
                    z.RBParent.RBParent.Color = Color.Red;
                    RotateLeft(z.RBParent.RBParent);
                }
            }

            RBRoot.Color = Color.Black;
        }

        private void RemoveFixup(RBTreeNode<T> x)
        {
            while (!IsRoot(x) && x.Color == Color.Black)
            {
                if (x == x.RBParent.Left)
                {
                    var w = x.RBParent.RBRight;
                    if (w.Color == Color.Red)
                    {
                        w.Color = Color.Black;
                        x.RBParent.Color = Color.Red;
                        RotateLeft(x.RBParent);
                        w = x.RBParent.RBRight;
                    }

                    if (w.RBLeft.Color == Color.Black && w.RBRight.Color == Color.Black)
                    {
                        w.Color = Color.Red;
                        x = x.RBParent;
                    }
                    else if (w.RBRight.Color == Color.Black)
                    {
                        w.RBLeft.Color = Color.Black;
                        w.Color = Color.Red;
                        RotateRight(w);
                        w = x.RBParent.RBRight;
                    }

                    w.Color = x.RBParent.Color;
                    x.RBParent.Color = Color.Black;
                    w.RBRight.Color = Color.Black;
                    RotateLeft(x.RBParent);
                    x = RBRoot;
                }
                else
                {
                    var w = x.RBParent.RBLeft;
                    if (w.Color == Color.Red)
                    {
                        w.Color = Color.Black;
                        x.RBParent.Color = Color.Red;
                        RotateRight(x.RBParent);
                        w = x.RBParent.RBLeft;
                    }

                    if (w.RBLeft.Color == Color.Black && w.RBRight.Color == Color.Black)
                    {
                        w.Color = Color.Red;
                        x = x.RBParent;
                    }
                    else if (w.RBLeft.Color == Color.Black)
                    {
                        w.RBRight.Color = Color.Black;
                        w.Color = Color.Red;
                        RotateLeft(w);
                        w = x.RBParent.RBLeft;
                    }

                    w.Color = x.RBParent.Color;
                    x.RBParent.Color = Color.Black;
                    w.RBLeft.Color = Color.Black;
                    RotateRight(x.RBParent);
                    x = RBRoot;
                }
            }

            x.Color = Color.Black;
        }

        #endregion

    }
}
