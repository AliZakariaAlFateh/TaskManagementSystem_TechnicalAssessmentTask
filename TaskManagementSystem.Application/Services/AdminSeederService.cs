using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Services
{
    public class AdminSeederService : IAdminSeederService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminSeederService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAdminAsync()
        {
            //if the admin not exist before add admin from the begining ..
            var adminUser = await _userManager.FindByEmailAsync("admin@gmail.com");

            if (adminUser == null)
            {
                // Create Admin user
                adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@gmail.com"
                };

                var result = await _userManager.CreateAsync(adminUser, "Admin@123");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
