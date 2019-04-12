namespace LlvmSharpLang {
    public struct Token {
        public readonly TokenType Type;

        public readonly string Value;

        public readonly int StartPos;

        public int EndPos {
            get => this.StartPos + this.Value.Length;
        }
    }
}
