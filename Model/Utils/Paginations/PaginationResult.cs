using System.Collections.Generic;

namespace DataLayer.Utils.Paginations
{
    public class PaginationResult<TEntity> where TEntity : class
    {
        public int ActualPage { get; set; } = 1;
        public int Total { get; set; }
        public int Pages { get; set; }
        public int Qyt { get; set; } = 15;
        public IEnumerable<TEntity> Results { get; set; }
    }
}
