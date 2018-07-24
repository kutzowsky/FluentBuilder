using FluentBuilder.Exceptions;
using FluentBuilder.Extensions;
using System;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace FluentBuilder
{
    public class FluentBuilder<T> : DynamicObject where T : class, new()
    {
        private T builtObject;
        readonly PropertyInfo[] builtObjectProperties;

        public FluentBuilder()
        {
            builtObject = new T();
            builtObjectProperties = builtObject.GetType().GetProperties();
        }

        public T Get()
        {
            return builtObject;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var methodName = binder.Name;
            CheckPrefixOf(methodName);

            var propertyName = RemovePrefixFrom(methodName);
            var property = FindPropertyWith(propertyName);

            var value = GetValueToSetFrom(args);
            CheckArgumentType(value, property);

            try
            {
                property.SetValue(builtObject, value);
            }
            catch (TargetInvocationException exc) 
            {
                throw exc.InnerException;    //to throw exception from setter which seems to be more natural result           
            }
            
            result = this;
            return true;
        }

        private void CheckPrefixOf(string methodName)
        {
            if (!methodName.StartsWith("With")) throw new InvalidMethodPrefixException();
        }

        private string RemovePrefixFrom(string methodName)
        {
            return methodName.Remove(0, 4);
        }

        private PropertyInfo FindPropertyWith(string propertyName)
        {
            var property = builtObjectProperties.FirstOrDefault(prop => prop.Name == propertyName);
            if (property == null) throw new NoSuchPropertyException(propertyName);

            return property;
        }

        private object GetValueToSetFrom(object[] args)
        {
            if (args.Count() != 1) throw new InvalidArgumentNumberException();

            return args.First();
        }

        private void CheckArgumentType(object value, PropertyInfo property)
        {
            var propertyType = property.PropertyType;

            if (value == null && !propertyType.IsPrimitive) return;

            if (propertyType.IsNullable())
            {
                var propertyUnderlyingType = Nullable.GetUnderlyingType(propertyType);
                if (!value.CanChangeTypeTo(propertyUnderlyingType)) throw new InvalidArgumentTypeException();
            }
            else
            {
                if (!value.CanChangeTypeTo(propertyType)) throw new InvalidArgumentTypeException();
            }
        }
    }
}
