using System;

namespace Commons.Helpers
{
    public static class StringHelper
    {
        public static string GetRandomCode(int length = 6) => Guid.NewGuid().ToString()[length..];
    }
}
