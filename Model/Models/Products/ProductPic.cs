using DataLayer.Models.Base;
using System;

namespace Model.Models
{
    public class ProductPic : BaseModel<int>
    {
        public string PicName { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
