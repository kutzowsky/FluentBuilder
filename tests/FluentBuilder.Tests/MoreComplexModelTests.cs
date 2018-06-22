using AutoFixture.Xunit2;
using FluentBuilder.Tests.TestModels;
using Shouldly;
using Xunit;

namespace FluentBuilder.Tests
{
    public class MoreComplexModelTests
    {
        [Theory, AutoData]
        public void Test(PersonExtendedInfo sourcePersonInfo)
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
    }
}
