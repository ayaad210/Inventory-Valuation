using System;
using System.Linq;
using System.Security.Claims;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace businessLayer.SeedingData
{
    public static class SeedData
    {

        public static async void SeedIdentityData(IServiceCollection services)
        {

            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var admin = userMgr.FindByNameAsync("Madmin@gmail.com").Result;
                if (admin == null)
                {
                    admin = new ApplicationUser
                    {
                        UserName = "Madmin@gmail.com",
                        Email = "Madmin@gmail.com",
                        EmailConfirmed = true,
                      
                    };
                    var result = userMgr.CreateAsync(admin, "As123456+").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    var role = new IdentityRole { Name = "admin", NormalizedName = "ADMIN" };
                 
                    var RoleCreateres = await roleMgr.CreateAsync(role);
                
                     
                    await userMgr.AddToRoleAsync(admin, "admin");

                  

                    Console.WriteLine("admin role created");
                    if (!await roleMgr.RoleExistsAsync("Employee"))
                    {
                        IdentityRole newRole = new IdentityRole("Employee");
                        await roleMgr.CreateAsync(newRole);
                    }
                }
                else
                {
                    Console.WriteLine("admin already exists");
                }

              
            }

 

      
        }


    }
}
