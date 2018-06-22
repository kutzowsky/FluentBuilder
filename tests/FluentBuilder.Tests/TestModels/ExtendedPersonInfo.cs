using System;

namespace FluentBuilder.Tests.TestModels
{
    public class PersonExtendedInfo : Person
    {
        public Guid Id { get; }

        public Address Address { get; set; }

        private decimal salary;
        public decimal Salary
        {
            get { return salary; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException($"{nameof(value)} cannot be negative.");
                }

                salary = value;
            }
        }
        
        public bool Active { get; set; } = true;
    }
}
