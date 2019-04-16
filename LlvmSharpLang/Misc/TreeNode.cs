namespace LlvmSharpLang.Misc
{
    public class TreeNode
    {
        public TreeNodeChildren Children { get; }

        protected readonly Tree tree;

        public TreeNode(Tree tree)
        {
            this.tree = tree;
            this.Children = new TreeNodeChildren();
        }
    }
}
