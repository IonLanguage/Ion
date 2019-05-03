using System.Collections.Generic;

namespace Ion.AST
{
    public class NodeChildren<T>
    {
        public NodeChildren()
        {
            //
        }

        public NodeChildren(Node<T> left, Node<T> right)
        {
            this.Left = left;
            this.Right = right;
        }

        public Node<T> Left { get; set; }

        public Node<T> Right { get; set; }

        public bool HasLeft
            => this.Left != null;

        public bool HasRight
            => this.Left != null;

        public bool HasAny
            => this.HasLeft || this.HasRight;

        public bool HasBoth
            => this.HasLeft && this.HasRight;

        /// <summary>
        ///     Whether both the left and right children
        ///     nodes do not exist and are unset.
        /// </summary>
        public bool Empty => this.HasAny;

        public void Clear()
        {
            this.Left = null;
            this.Right = null;
        }

        public List<Node<T>> GetOrdered()
        {
            var result = new List<Node<T>>();

            // Collect all children from the left node if applicable.
            if (this.HasLeft) result.AddRange(this.Left.Children.GetOrdered());

            // Collect all children from the right node if applicable.
            if (this.HasRight) result.AddRange(this.Right.Children.GetOrdered());

            return result;
        }
    }
}