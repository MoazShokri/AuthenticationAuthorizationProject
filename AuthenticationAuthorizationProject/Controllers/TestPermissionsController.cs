using AuthenticationAuthorizationProject.Constants;
using AuthenticationAuthorizationProject.Filter;
using AuthenticationAuthorizationProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAuthorizationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestPermissionsController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public TestPermissionsController(RoleManager<IdentityRole> roleManager )
        {
            this._roleManager = roleManager;
        }
        [Authorize(Permissions.Factory.View)]
        [HttpGet]
        public async Task<IActionResult> Get(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return NotFound();
            }
            var roleClaims = _roleManager.GetClaimsAsync(role).Result.Select(c => c.Value).ToList();

            if (roleClaims == null)
            {

                return NotFound();

            }
            return Ok("Allow Access On This Permission");

        }
      
    }
}
