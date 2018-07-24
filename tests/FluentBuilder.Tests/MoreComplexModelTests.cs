using AutoFixture.Xunit2;
using FluentBuilder.Tests.TestModels;
using Shouldly;
using System;
using Xunit;

namespace FluentBuilder.Tests
{
    public class MoreComplexModelTests
    {
        [Theory, AutoData]
        public void Builder_Should_ProperlyBuildMoreComplicatedObjects(PersonExtendedInfo sourcePersonInfo)
        {
            dynamic personInfoBuilder = new FluentBuilder<PersonExtendedInfo>();

            personInfoBuilder.WithActive(sourcePersonInfo.Active)
                .WithAdditionalData(sourcePersonInfo.AdditionalData)
                .WithAddress(sourcePersonInfo.Address)
                .WithAge(sourcePersonInfo.Age)
                .WithDateOfBirth(sourcePersonInfo.DateOfBirth)
                .WithDateOfDeath(sourcePersonInfo.DateOfDeath)
                .WithName(sourcePersonInfo.Name)
                .WithSurname(sourcePersonInfo.Surname)
                .WithSalary(sourcePersonInfo.Salary);

            PersonExtendedInfo builtPersonInfo = personInfoBuilder.Get();

            //TODO: find/write some Shouldly method to compare objects only by property values
            builtPersonInfo.Active.ShouldBe(sourcePersonInfo.Active);
            builtPersonInfo.AdditionalData.ShouldBe(sourcePersonInfo.AdditionalData);
            builtPersonInfo.Address.ShouldBe(sourcePersonInfo.Address);
            builtPersonInfo.Age.ShouldBe(sourcePersonInfo.Age);
            builtPersonInfo.DateOfBirth.ShouldBe(sourcePersonInfo.DateOfBirth);
            builtPersonInfo.DateOfDeath.ShouldBe(sourcePersonInfo.DateOfDeath);
            builtPersonInfo.Name.ShouldBe(sourcePersonInfo.Name);
            builtPersonInfo.Surname.ShouldBe(sourcePersonInfo.Surname);
            builtPersonInfo.Salary.ShouldBe(sourcePersonInfo.Salary);
        }

        [Fact]
        public void Builder_Should_UsePropertySetters()
        {
            dynamic personInfoBuilder = new FluentBuilder<PersonExtendedInfo>();

            Should.Throw<ArgumentOutOfRangeException>(() =>
            {
                personInfoBuilder.WithSalary(-1000m);
            });
        }

        [Fact]
        public void Builder_Should_CanIntoEnums()
        {
            var expectedArea = Areas.Suburban;
            dynamic addressBuilder = new FluentBuilder<Address>();

            addressBuilder.WithArea(expectedArea);

            Address address = addressBuilder.Get();

            address.Area.ShouldBe(Areas.Suburban);
        }

        [Theory, AutoData]
        public void Builder_Should_BeAbleToHandleGenerics(int sourceNumber, SomeClass sourceSomeClass, Person sourcePerson)
        {
            dynamic genericClassBuilder = new FluentBuilder<SoMuchGenericClass<int, SomeClass, Person>>();

            genericClassBuilder.WithFirstProperty(sourceNumber).WithSecondProperty(sourceSomeClass).WithThirdProperty(sourcePerson);

            SoMuchGenericClass<int, SomeClass, Person> genericClass = genericClassBuilder.Get();

            genericClass.FirstProperty.ShouldBe(sourceNumber);
            genericClass.SecondProperty.ShouldBe(sourceSomeClass);
            genericClass.ThirdProperty.ShouldBe(sourcePerson);
        }

        [Fact]
        public void Builder_Should_NotBeAbleToSetValueOfPropertyWithoutSetter()
        {
            dynamic personExtendedInfoBuilder = new FluentBuilder<PersonExtendedInfo>(); 

            Should.Throw<ArgumentException>(() =>
            {
                personExtendedInfoBuilder.WithId(Guid.NewGuid());
            });
        }
    }
}
