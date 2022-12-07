using JunityEngine;

namespace Bounce.Gameplay.Domain.Runtime
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