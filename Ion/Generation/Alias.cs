namespace Ion.Generation
{
    public class Alias
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
