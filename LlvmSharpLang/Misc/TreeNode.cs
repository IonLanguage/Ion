namespace LlvmSharpLang.Misc
{
    public class TreeNode
    {
        public TreeNodeChildren Children { get; }

        protected readonly Tree<TreeNode> tree;

        public TreeNode(Tree<TreeNode> tree)
        {
            this.tree = tree;
            this.Children = new TreeNodeChildren();
        }
    }
}
