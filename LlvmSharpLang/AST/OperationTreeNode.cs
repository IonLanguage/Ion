using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Misc
{
    public class OperationTreeNode : TreeNode
    {
        public TokenType Operator { get; set; }

        public OperationTreeNode(Tree<TreeNode> tree) : base(tree)
        {
            //
        }
    }
}
