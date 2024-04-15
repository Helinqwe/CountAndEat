using CoundAndEat.Dtos.Auth;

namespace CoundAndEat.Api.Interfaces
{
    public interface IAdminService
    {
        Task<Status> SeedRolesAsync();
        Task<Status> MakeAdminAsync(UpdatePermissionDto updatePermissionDto);
    }
}
