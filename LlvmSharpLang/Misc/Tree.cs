using System.Collections.Generic;

namespace LlvmSharpLang.Misc
{
    public class Tree<T> where T : TreeNode
    {
        public List<T> Nodes { get; protected set; }

        public Tree()
        {
            this.Nodes = new List<T>();
        }
    }
}
