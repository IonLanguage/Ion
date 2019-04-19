using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.AST
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
