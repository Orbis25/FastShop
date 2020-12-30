using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels
{
    public class ProductPaginationVM 
    {
        public IEnumerable<Product> Products { get; set; }
        public int ActualPage { get; set; }
        public int TotalOfRegisters { get; set; }
        public int RegisterByPage { get; set; }
    }
}
