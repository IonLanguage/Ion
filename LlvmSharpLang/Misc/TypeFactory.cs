using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.CognitiveServices;

namespace LlvmSharpLang.Misc
{
    public class TypeFactory
    {
        public static Type Int32()
        {
            return new Type(TypeName.Int32);
        }

        public static Type Int64()
        {
            return new Type(TypeName.Int64);
        }

        public static Type Character()
        {
            return new Type(TypeName.Character);
        }

        public static Type Float()
        {
            return new Type(TypeName.Float);
        }

        public static Type Double()
        {
            return new Type(TypeName.Double);
        }

        public static Type Boolean()
        {
            return new Type(TypeName.Boolean);
        }

        public static Type Void()
        {
            return new Type(TypeName.Void);
        }
    }
}
