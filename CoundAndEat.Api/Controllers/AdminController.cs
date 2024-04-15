using CoundAndEat.Api.Interfaces;
using CoundAndEat.Dtos.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CoundAndEat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost]
        [Route("seed-roles")]
        public async Task<IActionResult> SeedRoles()
        {
            var seerRoles = await _adminService.SeedRolesAsync();

            return Ok(seerRoles);
        }

        [HttpPost]
        [Route("make-admin")]
        public async Task<IActionResult> MakeAdmin([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var operationResult = await _adminService.MakeAdminAsync(updatePermissionDto);

            if (operationResult.StatusCode == 200)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }
    }
}
