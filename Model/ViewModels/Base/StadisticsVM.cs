using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels.Base
{
    public class StadisticsVM
    {
        public string Label { get; set; }
        public decimal Data { get; set; }
        public string DataStr => Data.ToString("C");
    }
}
