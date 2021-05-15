using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Commons.Helpers
{
    public static class DateHelper
    {
        public static string GetSpanishMonthName (int month) 
            => new CultureInfo("es-Es", false)
            .DateTimeFormat.GetMonthName(month).ToUpper();
        
    }
}
