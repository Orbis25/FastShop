using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class ProductPic : CommonProperties<int>
    {
        public Guid ProductId { get; set; }
        public string PicName { get; set; } 
    }
}
