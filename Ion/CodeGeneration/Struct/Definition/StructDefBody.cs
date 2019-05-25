using System.Collections.Generic;

namespace Ion.CodeGeneration
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
