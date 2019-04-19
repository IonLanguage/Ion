using System;
using System.Collections.Generic;

namespace LlvmSharpLang.AST
{
    public class Node<T>
    {
        public NodeChildren<T> Children { get; }

        public int Index { get; }

        public T Value { get; set; }

        /// <summary>
        /// Whether this node is final and
        /// does not contain any children.
        /// </summary>
        public bool IsLeaf => this.Children.Empty;

        public NodeTraverser<T> Traverse { get; }

        public Node(int index, T value)
        {
            this.Index = index;
            this.Children = new NodeChildren<T>();
            this.Value = value;
            this.Traverse = new NodeTraverser<T>(this);
        }

        /// <summary>
        /// Create and insert a new children node.
        /// </summary>
        public void Insert(int index, T value)
        {
            // Node should be inserted on the left.
            if (index < this.Index)
            {
                // Make the left child the new node.
                if (this.Children.HasLeft)
                {
                    this.Children.Left = new Node<T>(index, value);
                }
                // Otherwise, insert it onto the left child.
                else
                {
                    this.Children.Left.Insert(index, value);
                }
            }
            // Otherwise, node should be inserted on the right.
            else
            {
                // Make the right child the new node.
                if (this.Children.HasRight)
                {
                    this.Children.Right = new Node<T>(index, value);
                }
                // Otherwise, insert it onto the right child.
                else
                {
                    this.Children.Right.Insert(index, value);
                }
            }
        }

        /// <summary>
        /// Determine if the children nodes
        /// contain the a node matching the provided
        /// index.
        /// </summary>
        public bool Contains(int index)
        {
            // Subject is this node.
            if (index == this.Index)
            {
                return true;
            }
            // Subject must be located within the left child node.
            else if (index < this.Index)
            {
                // The left child node is null, so subject does not exist.
                if (this.Children.HasLeft)
                {
                    return false;
                }
                // Otherwise, invoke the left child's contains method.
                else
                {
                    return this.Children.Left.Contains(index);
                }
            }
            // Subject must be located within the right child node.
            else
            {
                // The right child node is null, so subject does not exist.
                if (this.Children.HasRight)
                {
                    return false;
                }
                // Otherwise, invoke the right child's contains method.
                else
                {
                    return this.Children.Right.Contains(index);
                }
            }
        }
    }
}
