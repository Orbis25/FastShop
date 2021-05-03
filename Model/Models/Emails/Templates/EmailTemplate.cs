using DataLayer.Enums.Base;
using DataLayer.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Emails.Templates
{
    public class EmailTemplate : BaseModel<Guid>
    {
        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public string Title { get; set; }
        [Display(Name = "Cuerpo de plantilla")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public string Body { get; set; }
        public TemplateTypeEnum Type { get; set; }
    }
}
