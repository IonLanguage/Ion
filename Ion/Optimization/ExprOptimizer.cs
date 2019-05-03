using System.Collections.Generic;
using Ion.CodeGeneration;

namespace Ion.Optimization
{
    // TODO: Finish implementing.
    public class ExprOptimizer : IOptimizer<List<Expr>>
    {
        protected readonly List<Expr> expressions;

        public ExprOptimizer(List<Expr> expressions)
        {
            this.expressions = expressions;
        }

        public List<Expr> Optimize()
        {
            // Loop through all expressions.
            foreach (Expr expr in expressions)
            {
                // TODO
            }

            return expressions;
        }
    }
}