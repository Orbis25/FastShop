﻿using DataLayer.Models.Cart;
using DataLayer.Models.Products;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(70, ErrorMessage = "El {0} es demaciado largo.")]
        [Display(Name = "Nombre completo")]
        public string FullName { get; set; }
        [MaxLength(100, ErrorMessage = "La {0} es demaciada largo.")]
        [Display(Name = "Dirreción")]
        public string Address { get; set; }
        [Display(Name = "Imagen")]
        public string ProfileImage { get; set; }
        [Display(Name = "Pais")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public string Country { get; set; }
        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "La {0} es requerida")]
        public string City { get; set; }

        public ICollection<Sale> Sales { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
