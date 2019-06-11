using Ion.Engine.CodeGeneration.Helpers;

namespace Ion.CodeGeneration.Helpers
{
    public interface IContextPipe<TContext, TOutput> : IPipe<PipeContext<TContext>, TOutput>
    {
        //
    }
}
