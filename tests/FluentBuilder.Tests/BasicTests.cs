using FluentBuilder.Exceptions;
using Shouldly;
using Xunit;

namespace FluentBuilder.Tests
{
    public class BasicTests
    {
        class SomeClass
        {
            public int SomeNumber { get; set; }
            public string SomeString { get; set; }
        }

        [Fact]
        public void Get_Should_ReturnObjectOfTypePassedToBuilder()
        {
            dynamic builder = new FluentBuilder<SomeClass>();

            SomeClass obj = builder.Get();

            obj.ShouldBeOfType<SomeClass>();
        }

        [Fact]
        public void Builder_Should_AcceptOnlyMethodsLinkedToPropertyNames()
        {
            dynamic builder = new FluentBuilder<SomeClass>();

            Should.NotThrow(() =>
            {
                builder.WithSomeNumber(1);
                builder.WithSomeString("Is this the real life ?");
            });
        }

        [Fact]
        public void Builder_Should_NotAcceptOnlyMethodsNotLinkedToPropertyNames()
        {
            dynamic builder = new FluentBuilder<SomeClass>();

            Should.Throw<NoSuchPropertyException>(() =>
            {
                builder.WithSomeOtherProperty("Is this just fantasy?");
            });
        }

        [Fact]
        public void Builder_ShouldThrow_OnMethodsWithoutValidPrefix()
        {
            dynamic builder = new FluentBuilder<SomeClass>();

            Should.Throw<InvalidMethodPrefixException>(() =>
            {
                builder.MethodWithoutPrefix("Caught in a landslide");
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
            dynamic builder = new FluentBuilder<SomeClass>();

            //TODO: custom exception
            Should.Throw<InvalidArgumentNumberException>(() =>
            {
                builder.WithSomeNumber();
            });
        }

        [Fact]
        public void BuildMethods_ShouldThrow_When_MoreThanOneArgumentPassed()
        {
            dynamic builder = new FluentBuilder<SomeClass>();

            //TODO: custom exception
            Should.Throw<InvalidArgumentNumberException>(() =>
            {
                builder.WithSomeNumber(1, 2);
            });
        }

        [Fact]
        public void BuildMethods_ShouldThrow_When_ArgumentIsDifferrentTypeThanTheSourceObjectProperty()
        {
            dynamic builder = new FluentBuilder<SomeClass>();

            //TODO: custom exception
            Should.Throw<InvalidArgumentTypeException>(() =>
            {
                builder.WithSomeNumber("No escape from reality");
            });
        }

        [Fact]
        public void BuildMethods_ShouldSet_CorrectPropetyValuesInTheSourceObject()
        {
            const int expectedNumber = 42;
            const string expectedString = "I'm just a poor string, I need no sympathy.";

            dynamic builder = new FluentBuilder<SomeClass>();
            builder.WithSomeNumber(expectedNumber).WithSomeString(expectedString);
            SomeClass someClassInstance = builder.Get();

            someClassInstance.SomeNumber.ShouldBe(expectedNumber);
            someClassInstance.SomeString.ShouldBe(expectedString);
        }
    }
}
