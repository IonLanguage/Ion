namespace Ion.CodeGeneration
{
    public enum StatementType
    {
        Declaration,

        Assignment,

        Return,

        Expression,

        FunctionCall,

        Struct
    }

    public interface IStatement
    {
        StatementType StatementType { get; }
    }
}
