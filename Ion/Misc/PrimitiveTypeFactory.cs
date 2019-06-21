using Ion.Generation;
using Ion.CognitiveServices;

namespace Ion.Misc
{
    public static class PrimitiveTypeFactory
    {
        public static PrimitiveType Int32()
        {
            return new PrimitiveType(TypeName.Int32);
        }

        public static PrimitiveType Int64()
        {
            return new PrimitiveType(TypeName.Int64);
        }

        public static PrimitiveType Character()
        {
            return new PrimitiveType(TypeName.Character);
        }

        public static PrimitiveType Float()
        {
            return new PrimitiveType(TypeName.Float);
        }

        public static PrimitiveType Double()
        {
            return new PrimitiveType(TypeName.Double);
        }

        public static PrimitiveType Boolean()
        {
            return new PrimitiveType(TypeName.Boolean);
        }

        public static PrimitiveType Void()
        {
            return new PrimitiveType(TypeName.Void);
        }

        public static PrimitiveType String()
        {
            return new PrimitiveType(TypeName.String);
        }
    }
}
