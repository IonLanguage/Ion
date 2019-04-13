using LLVMSharp;

namespace LlvmSharpLang
{
    public abstract class NamedEntity<T> : IEntity<T>
    {
        public string Name { get; protected set; } = "anonymous";

        public abstract T Emit(LLVMModuleRef module);

        public void SetName(string name)
        {
            // TODO: Validate name here.
            this.Name = name;
        }
    }
}
