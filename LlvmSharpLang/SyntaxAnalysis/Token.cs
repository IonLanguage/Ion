namespace LlvmSharpLang.SyntaxAnalysis
{
    public struct Token
    {
        public TokenType Type;

        public string Value;

        public int StartPos;

        public int EndPos
        {
            get => this.StartPos + this.Value.Length;
        }

        public override string ToString()
        {
            return $"[{this.Type}:'{this.Value}'@{this.StartPos}-{this.EndPos}]";
        }
    }
}
