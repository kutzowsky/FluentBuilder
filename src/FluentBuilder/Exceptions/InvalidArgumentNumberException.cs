using System;

namespace FluentBuilder.Exceptions
{
    public class InvalidArgumentNumberException : Exception
    {
        public InvalidArgumentNumberException() 
            : base("There should be exactly one argument")
        { }
    }
}
