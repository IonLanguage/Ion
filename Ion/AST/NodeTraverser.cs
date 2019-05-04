using System;
using System.Collections.Generic;

namespace Ion.AST
{
    public class NodeTraverser<T>
    {
        protected readonly Node<T> node;

        public NodeTraverser(Node<T> node)
        {
            this.node = node;
        }

        /// <summary>
        ///     Traverse a node and its children, visiting all
        ///     the nodes of a level before moving to the next level.
        /// </summary>
        public void BreadthFirst(Action<Node<T>> callback)
        {
            // Create the node queue to loop.
            Queue<Node<T>> queue = new Queue<Node<T>>();

            // Add the initial node to the queue.
            queue.Enqueue(this.node);

            // Loop while the queue is not empty.
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                // Invoke the callback handler.
                callback(node);

                // Enqueue the left node if applicable.
                if (node.Children.HasLeft)
                {
                    queue.Enqueue(node.Children.Left);
                }

                // Enqueue the right node if applicable.
                if (node.Children.HasRight)
                {
                    queue.Enqueue(node.Children.Right);
                }
            }
        }
    }
}
