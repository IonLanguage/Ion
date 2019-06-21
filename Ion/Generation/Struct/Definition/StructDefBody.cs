using System.Collections.Generic;

namespace Ion.Generation
{
    public class StructDefBody
    {
        public List<StructDefProperty> Properties { get; }

        public StructDefBody(List<StructDefProperty> properties)
        {
            this.Properties = properties;
        }
    }
}
