using FluentBuilder.Exceptions;
using FluentBuilder.Tests.TestModels;
using Shouldly;
using System;
using Xunit;

namespace FluentBuilder.Tests
{
    public class NulllValuesTests
    {
        private readonly dynamic _personBuilder;

        public NulllValuesTests()
        {
            _personBuilder = new FluentBuilder<Person>();
        }

        [Fact]
        public void BuiltObject_ShouldHaveDevaultValues_When_Initialized()
        {
            Person result = _personBuilder.Get();

            result.Name.ShouldBeNull();
            result.Surname.ShouldBeNull();
            result.Age.ShouldBe(0);
            result.DateOfBirth.ShouldBe(DateTime.MinValue);
            result.DateOfDeath.ShouldBeNull();
            result.AdditionalData.ShouldBeNull();
        }

        [Fact]
        public void BuiltMethods_ShouldNotThrow_When_NullIsAssignedToNullableType()
        {
            Should.NotThrow(() =>
            {
                _personBuilder.WithName(null).WithDateOfDeath(null);
            });
        }

        [Fact]
        public void BuiltMethods_ShouldNotThrow_When_NullIsAssignedToRefferenceType()
        {
            Should.NotThrow(() =>
            {
                _personBuilder.WithAdditionalData(null);
            });
        }

        [Fact]
        public void BuiltMethods_ShouldNotThrow_WhenValueIsAssignedToNullableType()
        {
            Should.NotThrow(() =>
            {
                _personBuilder.WithName("Freddie").WithDateOfDeath(new DateTime(1991,11,24));
            });
        }

        [Fact]
        public void BuiltMethods_ShouldNotThrow_When_ValueIsAssignedToRefferenceType()
        {
            Should.NotThrow(() =>
            {
                _personBuilder.WithAdditionalData(new object());
            });
        }

        [Fact]
        public void BuiltMethods_ShouldThrow_When_NullIsAssignedToValueType()
        {
            Should.Throw<InvalidArgumentTypeException>(() =>
            {
                _personBuilder.WithAge(null);
            });
        }

        [Fact]
        public void BuiltMethods_ShouldThrow_When_ValueIsAssignedToValueType()
        {
            Should.NotThrow(() =>
            {
                _personBuilder.WithAge(45);
            });
        }
    }
}
