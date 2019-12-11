using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Model.Models;
using OnlineShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataSeeding
{
    public static class DataSeeder
    {
        public static void SeedRoles(IApplicationBuilder app)
        {

            using (var appScoped = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {

                ApplicationDbContext _context = appScoped.ServiceProvider.GetService<ApplicationDbContext>();
                UserManager<ApplicationUser> _userManager = appScoped.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                if (!_context.Roles.Any())
                {
                    var listRoles = new List<IdentityRole> { 
                       new IdentityRole { Name = "admin" , NormalizedName = "ADMIN" },
                       new IdentityRole { Name = "user" , NormalizedName = "USER" }
                    };

                    _context.AddRange(listRoles);
                    _context.SaveChanges();
                }
                if (!_context.ApplicationUsers.Any())
                {
                    var user = new ApplicationUser { UserName = "admin@admin.com", Email = "admin@admin.com", Address = "sample", PhoneNumber = "000000000", FullName = "admin" , EmailConfirmed = true };
                    _userManager.CreateAsync(user, "admin1234");
                    _context.Add(user);
                    _context.SaveChanges();

                    var role = _context.Roles.FirstOrDefault(x => x.Name.Equals("admin"));
                    _context.UserRoles.Add(new IdentityUserRole<string> { RoleId = role.Id , UserId = user.Id });
                    _context.SaveChanges();

                }

            }

        }
    }
}
