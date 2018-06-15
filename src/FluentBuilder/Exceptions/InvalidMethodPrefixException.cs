using System;

namespace FluentBuilder.Exceptions
{
    public class InvalidMethodPrefixException : Exception
    {
        public InvalidMethodPrefixException() 
            : base("Builder methods should have \"With\" prefix e.g \"WithAge\"")
        { }
    }
}
