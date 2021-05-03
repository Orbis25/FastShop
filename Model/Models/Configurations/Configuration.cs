using DataLayer.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Configurations
{
    public class Configuration : BaseModel<Guid>
    {
        [Required]
        public string EmailSender { get; set; }
        [Required]
        public string PasswordEmail { get; set; }
    }
}
