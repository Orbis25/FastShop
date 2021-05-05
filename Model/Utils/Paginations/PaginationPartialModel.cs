using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Utils.Paginations
{
    public class PaginationPartialModel
    {
        public int ActualPage { get; set; }
        public int Total { get; set; }
        public int Qyt { get; set; }
        public bool RenderOneInFirstPage { get; set; }
        public string Url { get; set; }
        public string IdToLoad { get; set; }
    }
}
