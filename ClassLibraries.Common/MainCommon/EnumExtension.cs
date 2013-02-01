using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MainCommon
{

    public static class EnumExtension
    {
        public static string GetStringValue<T>(this T value)
        {
            // Get the type
            Type type = typeof(T);

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                 typeof(StringValueAttribute), false) as StringValueAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }

        public static T GetEnumValue<T>(this string value)
        {
            Type type = typeof(T);
            FieldInfo[] fieldInfo = type.GetFields();
            var info = fieldInfo.Where(fi => ((StringValueAttribute[])fi.GetCustomAttributes(typeof(StringValueAttribute), false)).
                Where(va => va.StringValue == value).SingleOrDefault() != null).SingleOrDefault();

            return (T)System.Enum.Parse(type, info.Name);
        }
    }
}

