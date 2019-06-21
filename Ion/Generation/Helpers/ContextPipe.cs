using Ion.Engine.CodeGeneration.Helpers;

namespace Ion.Generation.Helpers
{
    public interface IContextPipe<TContext, TOutput> : IPipe<PipeContext<TContext>, TOutput>
    {
        //
    }
}
