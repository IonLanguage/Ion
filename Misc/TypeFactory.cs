using LlvmSharpLang.CodeGen;

namespace LlvmSharpLang.Misc
{
    public class TypeFactory
    {
        public static Type Int32 => new Type(TypeName.Int32);

        public static Type Int64 => new Type(TypeName.Int64);

        public static Type Character => new Type(TypeName.Character);

        public static Type Float => new Type(TypeName.Float);

        public static Type Double => new Type(TypeName.Double);

        public static Type Boolean => new Type(TypeName.Boolean);
    }
}
