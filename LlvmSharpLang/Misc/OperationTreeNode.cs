namespace LlvmSharpLang.Misc
{
    public class OperationTreeNode : TreeNode
    {
        public OperationType Operation { get; set; }

        public OperationTreeNode(Tree tree) : base(tree)
        {
            //
        }
    }
}
