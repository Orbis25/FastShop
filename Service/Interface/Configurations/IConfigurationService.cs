using DataLayer.Models.Configurations;
using DataLayer.ViewModels.Configurations;
using Service.Interface;
using System;
using System.Threading.Tasks;

namespace BussinesLayer.Interface.Configurations
{
    public interface IConfigurationService : IBaseRepository<Configuration,Guid>
    {
        Task<ConfigurationEmailVM> GetEmailConfiguration();
    }
}
