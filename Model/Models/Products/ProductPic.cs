using DataLayer.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class ProductPic : BaseModel<int>
    {
        public string PicName { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
