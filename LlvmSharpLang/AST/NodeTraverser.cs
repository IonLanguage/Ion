using System;
using System.Collections.Generic;

namespace LlvmSharpLang.AST
{
    public class NodeTraverser<T>
    {
        protected readonly Node<T> node;

        public NodeTraverser(Node<T> node)
        {
            this.node = node;
        }

        public void BreadthFirst(Action<Node<T>> callback)
        {
            Queue<Node<T>> queue = new Queue<Node<T>>();

            // Add the initial node to the queue.
            queue.Enqueue(this.node);

            while (queue.Count > 0)
            {
                Node<T> node = queue.Dequeue();

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
