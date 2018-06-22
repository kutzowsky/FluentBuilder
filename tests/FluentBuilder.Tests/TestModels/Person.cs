using System;

namespace FluentBuilder.Tests.TestModels
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
}
