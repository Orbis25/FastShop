using DataLayer.Models.Base;
using Model.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Emails
{
    public class Email : BaseModel<Guid>
    {
        public string To { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
