using CoundAndEat.Api.Data;
using CoundAndEat.Api.Entities;
using CoundAndEat.Api.Interfaces;
using CoundAndEat.Dtos.Auth;
using Microsoft.AspNetCore.Identity;

namespace CoundAndEat.Api.Service
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<long>> _roleManager;
        private readonly DataContext _context;

        public AdminService(DataContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<long>> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Status> MakeAdminAsync(UpdatePermissionDto updatePermissionDto)
        {
            var user = await _userManager.FindByEmailAsync(updatePermissionDto.Email);

            if (user is null)
                return new Status()
                {
                    StatusCode = 404,
                    Message = "Invalid User name!!!!!!!!"
                };

            await _userManager.AddToRoleAsync(user, UserRoles.Admin);

            return new Status()
            {
                StatusCode = 200,
                Message = "User is now an ADMIN"
            };
        }

        public async Task<Status> SeedRolesAsync()
        {
            bool isUserRoleExists = await _roleManager.RoleExistsAsync(UserRoles.User);
            bool isAdminRoleExists = await _roleManager.RoleExistsAsync(UserRoles.Admin);

            if (isUserRoleExists && isAdminRoleExists)
                return new Status()
                {
                    StatusCode = 200,
                    Message = "Roles Seeding is Already Done"
                };

            await _roleManager.CreateAsync(new IdentityRole<long>(UserRoles.User));
            await _roleManager.CreateAsync(new IdentityRole<long>(UserRoles.Admin));

            return new Status()
            {
                StatusCode = 200,
                Message = "Role Seeding Done Successfully"
            };
        }
    }
}
