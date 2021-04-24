using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels.Products.ProductPics
{
    public class ProductPicRemoveVM
    {
        public int Id { get; set; }
        public Guid ProductId { get; set; }
        public string Path { get; set; }
    }
}
