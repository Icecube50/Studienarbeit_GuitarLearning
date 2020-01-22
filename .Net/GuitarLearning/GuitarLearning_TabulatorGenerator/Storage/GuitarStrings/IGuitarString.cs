using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator.Storage.GuitarStrings
{
    public abstract class IGuitarString
    {
        public abstract GuitarStringType StringType { get; set; }
        public abstract string StringName { get; set; }
        public abstract int CalculateTop(int amountOfStrings);
    }

    public static class ClassicalGuitar
    {
        public static IGuitarString E { get; private set; } = new String_E("String_E");
        public static IGuitarString A { get; private set; } = new String_A("String_A");
        public static IGuitarString D { get; private set; } = new String_D("String_D");
        public static IGuitarString G { get; private set; } = new String_G("String_G");
        public static IGuitarString B { get; private set; } = new String_B("String_B");
        public static IGuitarString HighE { get; private set; } = new String_HighE("String_HighE");

        public static IGuitarString EnumToObj(GuitarStringType stringType)
        {
            if (stringType == GuitarStringType.E) return E;
            if (stringType == GuitarStringType.A) return A;
            if (stringType == GuitarStringType.D) return D;
            if (stringType == GuitarStringType.G) return G;
            if (stringType == GuitarStringType.B) return B;
            if (stringType == GuitarStringType.e) return HighE;
            return null;
        }
    }

    public enum GuitarStringType
    {
        E,
        A,
        D,
        G,
        B,
        e
    };
}
