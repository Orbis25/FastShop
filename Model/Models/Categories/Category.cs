using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class Category : CommonProperties<int>
    {
        [Required]
        public string Name { get; set; }
    }
}
