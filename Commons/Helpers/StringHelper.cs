﻿using System;
using System.Text;

namespace Commons.Helpers
{
    public static class StringHelper
    {
        public static string GetRandomCode(int length = 6) => Guid.NewGuid().ToString().Substring(0, length);
        public static DateTime ToDate(this string value)
        {
            if (string.IsNullOrEmpty(value)) return DateTime.Now;
            try
            {
                return DateTime.ParseExact(value,"dd/MM/yyyy",null);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        public static string ToBase64String(this string value) => Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        public static string GetFromBase64(this string value) => Encoding.UTF8.GetString(Convert.FromBase64String(value));
    }
}
