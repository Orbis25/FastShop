using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DataSeeding
{
    public static class DataSeeder
    {
        public static void SeedRoles(IApplicationBuilder app)
        {

            using (var appScoped = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {

                ApplicationDbContext _context = appScoped.ServiceProvider.GetService<ApplicationDbContext>();

                if (!_context.Roles.Any())
                {
                    var listRoles = new List<IdentityRole> { 
                       new IdentityRole { Name = "admin" , NormalizedName = "ADMIN" },
                       new IdentityRole { Name = "user" , NormalizedName = "USER" }
                    };

                    _context.AddRange(listRoles);
                    _context.SaveChanges();
                }
            }

        }
    }
}
