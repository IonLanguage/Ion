namespace LlvmSharpLang.Misc
{
    public class TreeNodeChildren
    {
        public TreeNode Left { get; protected set; }

        public TreeNode Right { get; protected set; }

        public TreeNodeChildren()
        {
            //
        }

        public TreeNodeChildren(TreeNode left, TreeNode right)
        {
            this.Left = left;
            this.Right = right;
        }

        public bool HasLeft
            => this.Left != null;

        public bool HasRight
            => this.Left != null;

        public bool HasAny
            => this.HasLeft || this.HasRight;

        public bool HasBoth
            => this.HasLeft && this.HasRight;

        public bool Empty => this.HasAny;

        public void Clear()
        {
            this.Left = null;
            this.Right = null;
        }

        public void SetLeft(TreeNode node)
        {
            this.Left = node;
        }

        public void SetRight(TreeNode node)
        {
            this.Right = node;
        }
    }
}
