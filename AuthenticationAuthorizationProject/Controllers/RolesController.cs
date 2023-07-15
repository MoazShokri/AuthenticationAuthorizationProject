using AuthenticationAuthorizationProject.Constants;
using AuthenticationAuthorizationProject.Models;
using AuthenticationAuthorizationProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AuthenticationAuthorizationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager , UserManager<ApplicationUser> userManager)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var Roles = await _roleManager.Roles.ToListAsync();
            return Ok(Roles);
        }
        [HttpPost]
        public async Task<IActionResult> Add(RoleFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(await _roleManager.Roles.ToListAsync());

            if (await _roleManager.RoleExistsAsync(model.Name))
            {
                ModelState.AddModelError("Name", "Role is exists!");
                return BadRequest(await _roleManager.Roles.ToListAsync());
            }

            await _roleManager.CreateAsync(new IdentityRole(model.Name.Trim()));

            return Ok(model);
        }
        [HttpGet("managepermissions")]
        public async Task<IActionResult> ManagePermissions(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
                return NotFound();

            var roleClaims = _roleManager.GetClaimsAsync(role).Result.Select(c => c.Value).ToList();
            var allClaims = Permissions.GenerateAllPermissions();
            var allPermissions = allClaims.Select(p => new CheckBoxViewModel { DisplayValue = p }).ToList();

            foreach (var permission in allPermissions)
            {
                if (roleClaims.Any(c => c == permission.DisplayValue))
                    permission.IsSelected = true;
            }

            var viewModel = new PermissionsFormViewModel
            {
                RoleId = roleId,
                RoleName = role.Name,
                RoleCalims = allPermissions
            };

            return Ok(viewModel);
        }
        [HttpPost("addpermissions")]
        public async Task<IActionResult> ManagePermissions(PermissionsFormViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);

            if (role == null)
                return NotFound();

            var roleClaims = await _roleManager.GetClaimsAsync(role);

            foreach (var claim in roleClaims)
                await _roleManager.RemoveClaimAsync(role, claim);

            var selectedClaims = model.RoleCalims.Where(c => c.IsSelected).ToList();

            foreach (var claim in selectedClaims)
                await _roleManager.AddClaimAsync(role, new Claim("Permission", claim.DisplayValue));

            return Ok(model);
        }
    }
}
