using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels
{
    public class ProductCategoryVM : Product
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}
