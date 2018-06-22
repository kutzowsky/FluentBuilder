using FluentBuilder.Exceptions;
using Shouldly;
using System;
using Xunit;

namespace FluentBuilder.Tests
{
    public class NulllValuesTests
    {
        class Person
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public int Age { get; set; }
            public DateTime DateOfBirth { get; set; }
            public DateTime? DateOfDeath { get; set; }
            public object AdditionalData { get; set; }
        }

        [Fact]
        public void BuiltObject_ShouldHaveDevaultValues_When_Initialized()
        {
            dynamic personBuilder = new FluentBuilder<Person>();

            Person result = personBuilder.Get();

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
            dynamic personBuilder = new FluentBuilder<Person>();

            Should.NotThrow(() =>
            {
                personBuilder.WithName(null).WithDateOfDeath(null);
            });
        }

        [Fact]
        public void BuiltMethods_ShouldNotThrow_When_NullIsAssignedToRefferenceType()
        {
            dynamic personBuilder = new FluentBuilder<Person>();

            Should.NotThrow(() =>
            {
                personBuilder.WithAdditionalData(null);
            });
        }

        [Fact]
        public void BuiltMethods_ShouldNotThrow_WhenValueIsAssignedToNullableType()
        {
            dynamic personBuilder = new FluentBuilder<Person>();

            Should.NotThrow(() =>
            {
                personBuilder.WithName("Freddie").WithDateOfDeath(new DateTime(1991,11,24));
            });
        }

        [Fact]
        public void BuiltMethods_ShouldNotThrow_When_ValueIsAssignedToRefferenceType()
        {
            dynamic personBuilder = new FluentBuilder<Person>();

            Should.NotThrow(() =>
            {
                personBuilder.WithAdditionalData(new object());
            });
        }

        [Fact]
        public void BuiltMethods_ShouldThrow_When_NullIsAssignedToValueType()
        {
            dynamic personBuilder = new FluentBuilder<Person>();

            Should.Throw<InvalidArgumentTypeException>(() =>
            {
                personBuilder.WithAge(null);
            });
        }

        [Fact]
        public void BuiltMethods_ShouldThrow_When_ValueIsAssignedToValueType()
        {
            dynamic personBuilder = new FluentBuilder<Person>();

            Should.NotThrow(() =>
            {
                personBuilder.WithAge(45);
            });
        }
    }
}
