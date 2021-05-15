using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels.Products
{
    public class ProductTopVM
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string ProductPic { get; set; }
        public decimal Rating { get; set; }
    }
}
