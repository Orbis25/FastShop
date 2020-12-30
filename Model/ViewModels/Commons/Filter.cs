using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels
{
    public class Filter
    {
        public int Index { get; set; } = 1;
        public int Take { get; set; } = 9;
        public string Parameter { get; set; } = string.Empty;
        public double From { get; set; } = 0;
        public double To { get; set; } = 0;
        public int? Category { get; set; } = null;

    }
}
