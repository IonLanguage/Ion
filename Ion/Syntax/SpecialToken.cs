namespace Ion.Syntax
{
    public static class SpecialToken
    {
        public static Token ProgramStart => new Token
        {
            Type = TokenType.ProgramEnd
        };

        public static Token ProgramEnd => new Token
        {
            Type = TokenType.ProgramEnd
        };
    }
}
