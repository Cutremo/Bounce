using JunityEngine;

namespace Bounce.Runtime.Domain
{
    partial record Trampoline
    {
        public static Trampoline Null => new NullTrampoline();

        record NullTrampoline : Trampoline, INull
        {
            public override string ToString()
            {
                return "Null";
            }
        }
    }
}