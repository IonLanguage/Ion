using System;
using LLVMSharp;
using Ion.CodeGeneration.Structure;

namespace Ion.CodeGeneration
{
    public enum StatementType
    {
        Declaration,

        Assignment,

        Return,

        Expression,

        FunctionCall
    }

    public interface IStatement
    {
        StatementType StatementType { get; }
    }
}
