using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OnlineShop.ExtensionMethods
{
    public static class EnumExtension
    {
        public static string GetAttribute(this Enum enumValue)
            => enumValue.GetType()?.GetMember(enumValue.ToString())?.FirstOrDefault()?.GetCustomAttribute<DisplayAttribute>(false)?.Name ?? enumValue.ToString();

    }
}
