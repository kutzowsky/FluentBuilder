using System;

namespace FluentBuilder.Extensions
{
    public static class ObjectExtensions
    {
        public static bool CanChangeTypeTo(this object inputObject, Type type)
        {
            try
            {
                Convert.ChangeType(inputObject, type);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
