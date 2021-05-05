using DataLayer.Models.Base;
using Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Products
{
    public class Review : BaseModel<int>
    {
        [Display(Name = "Comentario")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [MaxLength(200,ErrorMessage = "Por favor indica un {0} mas corto")]
        public string Comment { get; set; }

        [Display(Name = "Puntuación")]
        [Required(ErrorMessage = "La {0} es requerida")]
        public decimal Rating { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
