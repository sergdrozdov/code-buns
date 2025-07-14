using System.ComponentModel;
using System.Reflection;

namespace CodeBuns.Dotnet.Common.Helpers
{
    public static class EnumHelper<T> where T : struct
    {
        public static T Parse(string value)
        {
            T val;
            if (Enum.TryParse(value, true, out val))
                return val;

            return default(T);
        }

        public static string GetName(object value)
        {
            return Enum.GetName(typeof(T), value);
        }

        public static TEnum GetDefaultValue<TEnum>() where TEnum : struct, Enum
        {
            return (TEnum)GetDefaultValue(typeof(TEnum));
        }

        public static object GetDefaultValue(Type enumType)
        {
            var attribute = enumType.GetCustomAttribute<DefaultValueAttribute>(inherit: false);
            if (attribute != null)
                return attribute.Value;

            var innerType = enumType.GetEnumUnderlyingType();
            var zero = Activator.CreateInstance(innerType);
            if (enumType.IsEnumDefined(zero))
                return zero;

            var values = enumType.GetEnumValues();

            return values.GetValue(0);
        }
    }
}
