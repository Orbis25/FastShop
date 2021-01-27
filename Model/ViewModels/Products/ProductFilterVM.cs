using DataLayer.Utils.Paginations;
using Model.Models;
using System.Collections.Generic;

namespace DataLayer.ViewModels.Products
{
    public class ProductFilterVM : PaginationBase
    {

        public int? Category { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Money Range
        /// </summary>
        public double From { get; set; } = 0;

        /// <summary>
        /// Money Range
        /// </summary>
        public double To { get; set; } = 0;

        public IEnumerable<Product> Results { get; set; }
    }
} 