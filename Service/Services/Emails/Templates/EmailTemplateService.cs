using BussinesLayer.Interface.Emails.Templates;
using BussinesLayer.Repository;
using DataLayer.Enums.Base;
using DataLayer.Models.Emails.Templates;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Services.Emails.Templates
{
    public class EmailTemplateService : BaseRepository<EmailTemplate,ApplicationDbContext,Guid> , IEmailTemplateService
    {
        private readonly ApplicationDbContext _dbContext;
        public EmailTemplateService(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public string GetParameters(TemplateTypeEnum types)
        {
            return types switch
            {
                TemplateTypeEnum.AccountConfirmed => "{url}",
                TemplateTypeEnum.LockedUser => string.Empty,
                TemplateTypeEnum.PasswordRecovery => "{url}",
                TemplateTypeEnum.Bill => "{date},{user},{cuppon},{total},{code},{orderCode},{paymentType}",
                _ => string.Empty,
            };
        }

        public bool HaveRequireParameters(string body, TemplateTypeEnum types)
        {
            var values = GetParameters(types).Split(",");
            foreach (var item in values) if (!body.Contains(item)) return false;
            return true;           
            
        }

        public async Task<bool> ExistTemplate(TemplateTypeEnum type)
        {
            var result = await _dbContext.EmailTemplates.FirstOrDefaultAsync(x => x.Type == type);
            return result != null;
        }

        public async Task<EmailTemplate> GetByType(TemplateTypeEnum type)
        {
            return await _dbContext.EmailTemplates.FirstOrDefaultAsync(x => x.Type == type);
        }
    }
}
