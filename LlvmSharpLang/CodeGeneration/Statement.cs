using System;
using LLVMSharp;
using LlvmSharpLang.CodeGeneration.Structure;

namespace LlvmSharpLang.CodeGeneration
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
