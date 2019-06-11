using Ion.Engine.CodeGeneration.Helpers;

namespace Ion.CodeGeneration
{
    public class Alias : IReaction<Module>
    {
        public string TargetName { get; }

        public string AliasName { get; }

        public Alias(string targetName, string aliasName)
        {
            this.TargetName = targetName;
            this.AliasName = aliasName;
        }

        public void Invoke(Module context)
        {
            // TODO
        }
    }
}
