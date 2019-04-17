namespace LlvmSharpLang.Misc
{
    public class OperationTreeNode : TreeNode
    {
        public OperationType Operation { get; set; }

        public OperationTreeNode(Tree<TreeNode> tree) : base(tree)
        {
            //
        }
    }
}
