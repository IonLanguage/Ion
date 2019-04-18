namespace LlvmSharpLang.Misc
{
    public class TreeNode
    {
        public TreeNodeChildren Children { get; }

        public Tree<TreeNode> Tree { get; }

        public TreeNode(Tree<TreeNode> tree)
        {
            this.Tree = tree;
            this.Children = new TreeNodeChildren();
        }
    }
}
