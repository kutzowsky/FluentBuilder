using Xunit;
using FluentBuilder.Extensions;
using Shouldly;

namespace FluentBuilder.Tests.Extensions
{
    public class ObjectExtensionsTests
    {
        [Fact]
        public void CanChangeTypeTo_Should_ReturnTrue_WhenTestingChangeToSameType()
        {
            int someInteger = 3;
            var canChangeType = someInteger.CanChangeTypeTo(typeof(int));

            canChangeType.ShouldBeTrue();
        }

        [Fact]
        public void CanChangeTypeTo_Should_ReturnFalse_WhenTestingChangeToIncompatibleType()
        {
            var someString = "I see a little silhouetto of a man";
            var canChangeType = someString.CanChangeTypeTo(typeof(int));

            canChangeType.ShouldBeFalse();
        }

        [Fact]
        public void CanChangeTypeTo_Should_ReturnTrue_WhenTestingChangeToWiderType()
        {
            var someString = "Scaramouch, scaramouch will you do the fandango";
            var canChangeType = someString.CanChangeTypeTo(typeof(object));

            canChangeType.ShouldBeTrue();
        }

        [Fact]
        public void CanChangeTypeTo_Should_ReturnTrue_WhenACastExists()
        {
            double pi = 3.14;
            var canChangeType = pi.CanChangeTypeTo(typeof(int));

            canChangeType.ShouldBeTrue();
        }

        [Fact]
        public void CanChangeTypeTo_Should_ReturnTrue_WhenCanSimplyParseFromAString()
        {
            string almostPi = "3";
            var canChangeType = almostPi.CanChangeTypeTo(typeof(int));

            canChangeType.ShouldBeTrue();
        }
    }
}
