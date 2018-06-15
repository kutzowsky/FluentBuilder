using FluentBuilder.Exceptions;
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
            result = this;

            if (!binder.Name.StartsWith("With")) throw new InvalidMethodPrefixException();

            var propertyName = binder.Name.Remove(0, 4);

            var property = builtObjectProperties.FirstOrDefault(prop => prop.Name == propertyName);

            if (property == null) throw new NoSuchPropertyException(propertyName);

            if (args.Count() != 1) throw new InvalidArgumentNumberException();

            var value = args.First();
            var valueType = value.GetType();
            var propertyType = property.PropertyType;

            if (!valueType.IsAssignableFrom(propertyType)) throw new InvalidArgumentTypeException();

            property.SetValue(builtObject, value);
            
            return true;
        }
    }
}
