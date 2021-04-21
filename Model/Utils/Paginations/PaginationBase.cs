namespace DataLayer.Utils.Paginations
{
    public class PaginationBase
    {
        public virtual int Qyt { get; set; } = 10;
        public virtual int Page { get; set; } = 1;
        public virtual int? Pages { get; set; }
        public virtual int? Total { get; set; }
    }
}
