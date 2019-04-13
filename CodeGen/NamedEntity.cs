namespace LlvmSharpLang.CodeGen
{
    public abstract class NamedEntity<TResult, TContext> : IEntity<TResult, TContext>
    {
        public string Name { get; protected set; } = SpecialName.anonymous;

        public abstract TResult Emit(TContext context);

        public void SetName(string name)
        {
            // TODO: Validate name here.
            this.Name = name;
        }
    }
}
