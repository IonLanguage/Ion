using LLVMSharp;

namespace LlvmSharpLang {
    public interface IEntity<T>
    {
        void Initialize(LLVMModuleRef mod);
        
        T Emit();
    }

    public abstract class Entity<T> : IEntity<T> {
        protected readonly LLVMModuleRef mod;

        public void Initialize(LLVMModuleRef mod) {
            this.mod = mod;
        }

        public abstract T Emit();
    }
}
