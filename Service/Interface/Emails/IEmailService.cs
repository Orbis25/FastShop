using DataLayer.Models.Emails;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Interface.Emails
{
    public interface IEmailService : IBaseRepository<Email,Guid>
    {
        Task<bool> Send(Email model);
    }
}
