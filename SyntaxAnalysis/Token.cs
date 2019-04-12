namespace LlvmSharpLang {
    public struct Token {
        public TokenType Type;

        public string Value;

        public int StartPos;

        public int EndPos {
            get => this.StartPos + this.Value.Length;
        }
    }
}
