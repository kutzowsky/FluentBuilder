using FluentBuilder.Exceptions;
using FluentBuilder.Tests.TestModels;
using Shouldly;
using Xunit;

namespace FluentBuilder.Tests
{
    public class BasicFunctionalityTests
    {
        private readonly dynamic _builder;

        public BasicFunctionalityTests()
        {
            _builder = new FluentBuilder<SomeClass>();
        }

        [Fact]
        public void Get_Should_ReturnObjectOfTypePassedToBuilder()
        {
            SomeClass obj = _builder.Get();

            obj.ShouldBeOfType<SomeClass>();
        }

        [Fact]
        public void Builder_Should_AcceptOnlyMethodsLinkedToPropertyNames()
        {
            Should.NotThrow(() =>
            {
                _builder.WithSomeNumber(1);
                _builder.WithSomeString("Is this the real life ?");
            });
        }

        [Fact]
        public void Builder_Should_NotAcceptOnlyMethodsNotLinkedToPropertyNames()
        {
            Should.Throw<NoSuchPropertyException>(() =>
            {
                _builder.WithSomeOtherProperty("Is this just fantasy?");
            });
        }

        [Fact]
        public void Builder_ShouldThrow_OnMethodsWithoutValidPrefix()
        {
            Should.Throw<InvalidMethodPrefixException>(() =>
            {
                _builder.MethodWithoutPrefix("Caught in a landslide");
            });
        }

        [Fact]
        public void BuildMethods_Should_ReturnBuilderObject()
        {
            var builder = new FluentBuilder<SomeClass>();

            FluentBuilder<SomeClass> builderAfterMethodCall = ((dynamic) builder).WithSomeNumber(1);

            builderAfterMethodCall.ShouldBeSameAs(builder);
        }

        [Fact]
        public void BuildMethods_ShouldThrow_When_NoArgumentsPassed()
        {
            Should.Throw<InvalidArgumentNumberException>(() =>
            {
                _builder.WithSomeNumber();
            });
        }

        [Fact]
        public void BuildMethods_ShouldThrow_When_MoreThanOneArgumentPassed()
        {
            Should.Throw<InvalidArgumentNumberException>(() =>
            {
                _builder.WithSomeNumber(1, 2);
            });
        }

        [Fact]
        public void BuildMethods_ShouldThrow_When_ArgumentIsDifferrentTypeThanTheSourceObjectProperty()
        {
            Should.Throw<InvalidArgumentTypeException>(() =>
            {
                _builder.WithSomeNumber("No escape from reality");
            });
        }

        [Fact]
        public void BuildMethods_ShouldSet_CorrectPropetyValuesInTheSourceObject()
        {
            const int expectedNumber = 42;
            const string expectedString = "I'm just a poor string, I need no sympathy.";

            _builder.WithSomeNumber(expectedNumber).WithSomeString(expectedString);
            SomeClass someClassInstance = _builder.Get();

            someClassInstance.SomeNumber.ShouldBe(expectedNumber);
            someClassInstance.SomeString.ShouldBe(expectedString);
        }
    }
}
