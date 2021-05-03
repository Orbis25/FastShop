using Model.Models;
using System;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAccountService
    {
        Task<bool> BlockAndUnlockAccount(Guid id);
        Task<ApplicationUser> GetByEmail(string email);
        Task<bool> ValidateUser(string id);
        Task<bool> ChangePassword(string code, string newPassword);
        Task<string> GetEmailTemplateToCreateAccount(string userId);
        Task<string> GetEmailTemplateToRecoveryAccount(string currencyStamUser);

    }
}
