using BussinesLayer.Interface.Configurations;
using BussinesLayer.Repository;
using DataLayer.Models.Configurations;
using DataLayer.ViewModels.Configurations;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Services.Configurations
{
    public class ConfigurationService : BaseRepository<Configuration, ApplicationDbContext, Guid>, IConfigurationService
    {
        private readonly ApplicationDbContext _context;
        public ConfigurationService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ConfigurationEmailVM> GetEmailConfiguration()
            => await _context.Configurations.Select(x =>
               new ConfigurationEmailVM { EmailSender = x.EmailSender , PasswordEmail = x.PasswordEmail  })
                .FirstOrDefaultAsync();
    }
}
