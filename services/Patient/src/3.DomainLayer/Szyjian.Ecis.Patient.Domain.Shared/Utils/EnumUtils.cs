using System;
using System.ComponentModel;
using System.Reflection;

namespace Szyjian.Ecis.Patient.Domain.Shared
{
    public class EnumUtils
    {
        /// <summary>
        /// 获取枚举值描述
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(object enumValue)
        {
            string value = enumValue.ToString();
            if (string.IsNullOrEmpty(value))
            {
                return value ?? string.Empty;
            }

            Type type = enumValue.GetType();
            FieldInfo field = type.GetField(value);

            if (field != null)
            {
                object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (objs.Length == 0) return value;

                DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
                return descriptionAttribute.Description;
            }
            else
            {
                return value;
            }
        }
    }
}
