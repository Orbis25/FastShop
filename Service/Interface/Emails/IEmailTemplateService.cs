using DataLayer.Enums.Base;
using DataLayer.Models.Emails.Templates;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Interface.Emails.Templates
{
    public interface IEmailTemplateService : IBaseRepository<EmailTemplate,Guid>
    {
        string GetParameters(TemplateTypeEnum types);
        bool HaveRequireParameters(string body, TemplateTypeEnum types);
        Task<bool> ExistTemplate(TemplateTypeEnum type);
        Task<EmailTemplate> GetByType(TemplateTypeEnum type);

    }
}
