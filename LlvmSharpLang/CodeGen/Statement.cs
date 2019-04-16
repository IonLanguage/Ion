using System;
using LLVMSharp;
using LlvmSharpLang.CodeGen.Structure;

namespace LlvmSharpLang.CodeGen
{
    public enum StatementType
    {
        Declaration,

        Assignment,

        Return,

        Expression
    }

    public interface IStatement
    {
        StatementType StatementType { get; }
    }
}
