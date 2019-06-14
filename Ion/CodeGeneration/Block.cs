using System.Collections.Generic;
using Ion.CodeGeneration.Helpers;
using Ion.Engine.Misc;
using Ion.Misc;
using LLVMSharp;
using Ion.IR.Constructs;
using Ion.IR.Generation;

namespace Ion.CodeGeneration
{
    public enum BlockType
    {
        Default,

        Short
    }

    public class Block : Named, IContextPipe<IrBuilder, Section>
    {
        public readonly List<Expr> Expressions;

        public Expr ReturnExpr { get; set; }

        public bool HasReturnExpr => this.ReturnExpr != null;

        public BlockType Type { get; set; }

        // TODO: Find a better way to cache emitted values.
        public Section Current { get; protected set; }

        public Block()
        {
            this.Expressions = new List<Expr>();
            this.SetName(Ion.Core.GlobalNameRegister.GetBlock());
        }

        public Section Emit(PipeContext<IrBuilder> context)
        {
            // Create the block.
            Section block = new Section();

            // Create the block's statement list.
            List<Instruction> statements = new List<Instruction>();

            // Emit the expressions.
            foreach (Expr expr in this.Expressions)
            {
                context.Target.Emit(expr.Emit());
            }

            // No value was returned.
            if (!this.HasReturnExpr)
            {
                LLVM.BuildRetVoid(builder);
            }
            // Otherwise, emit the set return value.
            else
            {
                // Emit the return expression.
                LLVM.BuildRet(builder, this.ReturnExpr.Emit(builderContext));
            }

            // Cache emitted block.
            this.Current = block;

            // Return the block.
            return block;
        }

        public void SetNameEntry()
        {
            this.SetName(SpecialName.Entry);
        }
    }
}
