using Xunit;
using FluentBuilder.Extensions;
using Shouldly;

namespace FluentBuilder.Tests.Extensions
{
    public class TypeExtensionsTests
    {
        [Fact]
        public void IsNullable_Should_ReturnTrue_OnNullableTypes()
        { 
            var isNullable = typeof(int?).IsNullable();

            isNullable.ShouldBeTrue();
        }

        [Fact]
        public void IsNullable_Should_ReturnFalse_OnNonNullableTypes()
        {
            var isNullable = typeof(int).IsNullable();

            isNullable.ShouldBeFalse();
        }
    }
}
