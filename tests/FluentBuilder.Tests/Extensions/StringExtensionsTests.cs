using Xunit;
using FluentBuilder.Extensions;
using Shouldly;

namespace FluentBuilder.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("I see a little silhouetto of a man", "I see ", "a little silhouetto of a man")]
        [InlineData("Scaramouch, scaramouch will you do the fandango", "", "Scaramouch, scaramouch will you do the fandango")]
        [InlineData("Thunderbolt and lightning very very frightening me", "frightening", "Thunderbolt and lightning very very frightening me")]
        [InlineData("", "Gallileo, Gallileo", "")]
        [InlineData("Gallileo, Gallileo", "Gallileo", ", Gallileo")]
        [InlineData("Gallileo Figaro - magnifico", "Gallileo Figaro - magnifico", "")]
        public void RemovePrefix_Should_CorrectlyRemovePrefixFromString(string given, string prefix, string expected)
        {
            given.RemovePrefix(prefix).ShouldBe(expected);
        }
    }
}
