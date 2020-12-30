using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAccountService
    {
        Task<bool> BlockAndUnlockAccount(Guid id);
        Task<ApplicationUser> GetByEmail(string email);
        Task<bool> ValidateUser(string id);
        Task<bool> SendEmailConfirmation(string email);
    }
}
