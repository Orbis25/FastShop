namespace DataLayer.Utils.Paginations
{
    public class PaginationBase
    {
        public int Qyt { get; set; } = 10;
        public int Page { get; set; } = 1;
        public int? Pages { get; set; }
        public int? Total { get; set; }
    }
}
