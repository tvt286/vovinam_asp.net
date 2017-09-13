using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web.Mvc;
using Vovinam.Resources;

namespace Vovinam.Common
{
    public static class WebUtil
    {
        public static List<SelectListItem> SelectList<T>(int selected)
        {
            var result = System.Enum.GetValues(typeof(T)).Cast<object>().Select(x => new SelectListItem
            {
                Text = ((Enum) x).GetString(),
                Value = ((int) x).ToString(),
                Selected = (int) x == selected,
            }).ToList();
            return result;
        }

        public static string GetDescriptionGroup(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                    as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static string GetEnumString(this ResourceManager resourceManager, Enum enumValue)
        {
            if (enumValue == null)
            {
                return "";
            }
            return resourceManager.GetString(string.Format("{0}.{1}", enumValue.GetType().Name, enumValue.ToString()));
        }

        public static string GetString(this Enum enumValue)
        {
            var enumText = EnumText.ResourceManager.GetString(string.Format("{0}.{1}", enumValue.GetType().Name, enumValue));
            if (string.IsNullOrWhiteSpace(enumText))
            {
                enumText = EnumText.ResourceManager.GetString(string.Format("{0}", enumValue));
                if (string.IsNullOrWhiteSpace(enumText))
                {
                    enumText = enumValue.ToString();
                }
            }

            return enumText;
        }

        public static SelectList ToSelectList<TEnum>(this TEnum enumObj, bool hasSelected = true) 
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var result = System.Enum.GetValues(typeof(TEnum)).Cast<object>().Select(x => new SelectListItem
            {
                Text = ((Enum)x).GetString(),
                Value = ((int)x).ToString(),
            }).ToList();
            if (hasSelected)
            {
                return new SelectList(result, "Value", "Text", (int)(object)enumObj);
            }
            return new SelectList(result, "Value", "Text");
        }

        public static SelectList GetEnumSelectList<TEnum>(object defaultSelected = null)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var result = System.Enum.GetValues(typeof(TEnum)).Cast<object>().Select(x => new SelectListItem
            {
                Text = ((Enum)x).GetString(),
                Value = ((int)x).ToString(),
            }).ToList();
            if (defaultSelected != null)
            {
                return new SelectList(result, "Value", "Text", (int)defaultSelected);
            }
            return new SelectList(result, "Value", "Text");
        }
    }


}
