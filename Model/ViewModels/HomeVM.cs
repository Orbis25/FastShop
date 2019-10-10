using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels
{
    public class HomeVM
    {
        public Offert Offert { get; set; }
        public List<Product> Products { get; set; }
    }
}
