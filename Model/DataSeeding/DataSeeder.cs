using Commons.Helpers;
using DataLayer.Enums.Base;
using DataLayer.Utils.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Model.Models;
using Model.Settings;
using Newtonsoft.Json;
using OnlineShop.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Model.DataSeeding
{
    public static class DataSeeder
    {

        private static EmailTemplateConfiguration GetValuesEmailTemplate(string jsonFile)
        {
            using StreamReader r = new(jsonFile);
            string json = r.ReadToEnd();
            return JsonConvert.DeserializeObject<EmailTemplateConfiguration>(json);
        }

        private static void SeedEmailTemplate(ApplicationDbContext dbContext, string wwwrootPath)
        {

            var templates = GetValuesEmailTemplate($@"{wwwrootPath}//base//Configuration//email-template.json");

            var entity = dbContext.EmailTemplates;
            var t1 = entity.Any(x => x.Type == TemplateTypeEnum.AccountConfirmed);
            if (!t1)
            {
                var result = dbContext.EmailTemplates.Add(new()
                {
                    Title = TemplateTypeEnum.AccountConfirmed.GetDisplayName(),
                    Body = templates.AccountConfirmed.Trim(),
                    Type = TemplateTypeEnum.AccountConfirmed
                });
                dbContext.SaveChanges();
            }
            
            var t2 = entity.Any(x => x.Type == TemplateTypeEnum.LockedUser);
            
            if (!t2)
            {
                var result = dbContext.EmailTemplates.Add(new()
                {
                    Title = TemplateTypeEnum.LockedUser.GetDisplayName(),
                    Body = templates.LockedUser.Trim(),
                    Type = TemplateTypeEnum.LockedUser
                });
                dbContext.SaveChanges();
            }

            var t3 = entity.Any(x => x.Type == TemplateTypeEnum.Bill);

            if (!t3)
            {
                var result = dbContext.EmailTemplates.Add(new()
                {
                    Title = TemplateTypeEnum.Bill.GetDisplayName(),
                    Body = templates.Bill.Trim(),
                    Type = TemplateTypeEnum.Bill
                });
                dbContext.SaveChanges();
            }

            var t4 = entity.Any(x => x.Type == TemplateTypeEnum.PasswordRecovery);

            if (!t4)
            {
                var result = dbContext.EmailTemplates.Add(new()
                {
                    Title = TemplateTypeEnum.PasswordRecovery.GetDisplayName(),
                    Body = templates.PasswordRecovery.Trim(),
                    Type = TemplateTypeEnum.PasswordRecovery
                });
                dbContext.SaveChanges();
            }
        }

        public static void SeedService(IApplicationBuilder app, IConfiguration configuration, string wwwrootPath)
        {
            var adminValues = configuration.GetSection(nameof(InternalConfiguration));
            using var appScoped = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            ApplicationDbContext _context = appScoped.ServiceProvider.GetService<ApplicationDbContext>();
            UserManager<ApplicationUser> _userManager = appScoped.ServiceProvider.GetService<UserManager<ApplicationUser>>();

            if (!_context.Roles.Any())
            {
                var listRoles = new List<IdentityRole> {
                       new IdentityRole { Name = nameof(AuthLevel.Admin) , NormalizedName =  nameof(AuthLevel.Admin).ToUpper() },
                       new IdentityRole { Name =  nameof(AuthLevel.User) , NormalizedName =  nameof(AuthLevel.User).ToUpper() }
                    };

                _context.AddRange(listRoles);
                _context.SaveChanges();
            }
            if (!_context.ApplicationUsers.Any(x => x.Email == adminValues[nameof(InternalConfiguration.Admin)]))
            {
                var user = new ApplicationUser
                {
                    UserName = adminValues[nameof(InternalConfiguration.Admin)],
                    Email = adminValues[nameof(InternalConfiguration.Admin)],
                    Address = string.Empty,
                    PhoneNumber = adminValues[nameof(InternalConfiguration.AdminPws)],
                    FullName = adminValues[nameof(InternalConfiguration.Admin)],
                    EmailConfirmed = true,
                    City = adminValues[nameof(InternalConfiguration.City)],
                    Country = adminValues[nameof(InternalConfiguration.Country)]
                };
                _userManager.CreateAsync(user, adminValues[nameof(InternalConfiguration.AdminPws)]);
                _context.Add(user);
                _context.SaveChanges();

                var role = _context.Roles.FirstOrDefault(x => x.Name.Equals(nameof(AuthLevel.Admin)));
                _context.UserRoles.Add(new IdentityUserRole<string> { RoleId = role.Id, UserId = user.Id });
                _context.SaveChanges();

            }

            SeedEmailTemplate(_context, wwwrootPath);

        }
    }
}
