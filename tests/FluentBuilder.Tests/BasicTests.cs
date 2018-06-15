using Microsoft.CSharp.RuntimeBinder;
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
                builder.WithSomeString("Hello I'm a string");
            });
        }

        [Fact]
        public void Builder_Should_NotAcceptOnlyMethodsNotLinkedToPropertyNames()
        {
            dynamic builder = new FluentBuilder<SomeClass>();

            //TODO: custom exception
            Should.Throw<RuntimeBinderException>(() =>
            {
                builder.SomeNumber(1);
                builder.SomeStrangemethod("???");
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
        public void BuildMethods_ShouldThrow_When_NoParametersPassed()
        {
            dynamic builder = new FluentBuilder<SomeClass>();

            //TODO: custom exception
            Should.Throw<RuntimeBinderException>(() =>
            {
                builder.WithSomeNumber();
            });
        }

        [Fact]
        public void BuildMethods_ShouldThrow_When_MoreThanOneParameterPassed()
        {
            dynamic builder = new FluentBuilder<SomeClass>();

            //TODO: custom exception
            Should.Throw<RuntimeBinderException>(() =>
            {
                builder.WithSomeNumber(1, 2);
            });
        }

        [Fact]
        public void BuildMethods_ShouldThrow_When_ArgumentIsDifferrentTypeThanTheSourceObjectProperty()
        {
            dynamic builder = new FluentBuilder<SomeClass>();

            //TODO: custom exception
            Should.Throw<RuntimeBinderException>(() =>
            {
                builder.WithSomeNumber("String instead of number");
            });
        }
    }
}
