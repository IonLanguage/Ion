namespace Ion.Generation
{
    public class StringExpr : Construct
    {
        public override ConstructType ConstructType => ConstructType.String;

        public readonly string value;

        public StringExpr(string value)
        {
            this.value = value;
        }
    }
}
