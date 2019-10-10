using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels
{
    public class ShopVM
    {
        public IEnumerable<Category> Categories { get; set; }
        public ProductPaginationVM Products { get; set; }
        public Filter Filters { get; set; }
    }
}
