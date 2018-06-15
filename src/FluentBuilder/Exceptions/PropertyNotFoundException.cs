using System;

namespace FluentBuilder.Exceptions
{
    public class NoSuchPropertyException : Exception
    {
        public NoSuchPropertyException(string propertyName) 
            : base($"There's no public property named {propertyName} in the source object")
        { }
    }
}
