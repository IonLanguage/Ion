namespace Ion.AST
{
    public class Node<T>
    {
        public Node(int index, T value)
        {
            Index = index;
            Children = new NodeChildren<T>();
            Value = value;
            Traverse = new NodeTraverser<T>(this);
        }

        /// <summary>
        ///     A class containing the children
        ///     associated with this node.
        /// </summary>
        public NodeChildren<T> Children { get; }

        public int Index { get; }

        public T Value { get; set; }

        /// <summary>
        ///     Whether this node is final and
        ///     does not contain any children.
        /// </summary>
        public bool IsLeaf => Children.Empty;

        public NodeTraverser<T> Traverse { get; }

        /// <summary>
        ///     Create and insert a new children node.
        /// </summary>
        public void Insert(int index, T value)
        {
            // Node should be inserted on the left.
            if (index < Index)
            {
                // Make the left child the new node.
                if (Children.HasLeft)
                    Children.Left = new Node<T>(index, value);
                // Otherwise, insert it onto the left child.
                else
                    Children.Left.Insert(index, value);
            }
            // Otherwise, node should be inserted on the right.
            else
            {
                // Make the right child the new node.
                if (Children.HasRight)
                    Children.Right = new Node<T>(index, value);
                // Otherwise, insert it onto the right child.
                else
                    Children.Right.Insert(index, value);
            }
        }

        /// <summary>
        ///     Determine if the children nodes
        ///     contain the a node matching the provided
        ///     index.
        /// </summary>
        public bool Contains(int index)
        {
            // Subject is this node.
            if (index == Index)
            {
                return true;
            }
            // Subject must be located within the left child node.
            else if (index < Index)
            {
                // The left child node is null, so subject does not exist.
                if (Children.HasLeft)
                    return false;
                // Otherwise, invoke the left child's contains method.
                else
                    return Children.Left.Contains(index);
            }
            // Subject must be located within the right child node.
            else
            {
                // The right child node is null, so subject does not exist.
                if (Children.HasRight)
                    return false;
                // Otherwise, invoke the right child's contains method.
                else
                    return Children.Right.Contains(index);
            }
        }
    }
}