using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace OrientalMedical.Shared.Utilities.Helpers
{
    public static class EnumHelper
    {
        public static string GetEnumDescription(this Enum value)
        {
            var enumType = value.GetType();
            var field = enumType.GetField(value.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length == 0 ? value.ToString() : ((DescriptionAttribute)attributes[0]).Description;
        }

        public static IEnumerable<T> GetEnumValues<T>()
        {
            Type type = typeof(T);

            if (!type.IsEnum)
            {
                throw new Exception($"{type.FullName} is not an enum.");
            }

            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (FieldInfo item in fields)
            {
                yield return (T)item.GetValue(null);
            }

        }

    }
}
