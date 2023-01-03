using FluentAssertions;
using JunityEngine.Maths.Runtime;
using NUnit.Framework;

namespace JunityEngine.Maths.Tests
{
    public class Vector2Tests
    {
        [Test]
        public void NormalizeVector()
        {
            new Vector2(2, 0)
                .Normalize
                .Should().Be(new Vector2(1, 0));
        }
    }
}