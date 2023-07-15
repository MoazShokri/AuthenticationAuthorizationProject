using AuthenticationAuthorizationProject.Constants;
using AuthenticationAuthorizationProject.DataAccess.Data;
using AuthenticationAuthorizationProject.DataAccess.Repository.IRepository;
using AuthenticationAuthorizationProject.Dtos.GroupDto;
using AuthenticationAuthorizationProject.Dtos.GroupPermissionDto;
using AuthenticationAuthorizationProject.Dtos.PermissionDto;
using AuthenticationAuthorizationProject.Models;
using AuthenticationAuthorizationProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace AuthenticationAuthorizationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public RolesController(RoleManager<IdentityRole> roleManager , UserManager<ApplicationUser> userManager , IUnitOfWork unitOfWork)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._unitOfWork = unitOfWork;
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
        [HttpPost("addpermissions/Claims")]
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
        [HttpGet("getpermissions")]
        public async Task<IActionResult> GetPermissions()
        {
            var permissions = await _unitOfWork.Permission.GetAll();
            return Ok(permissions);
        }
        [HttpPost("permissions")]
        public IActionResult AddPermission([FromBody] AddPermissionDto permissionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var permission = new Permission
            {
                Name = permissionDto.Name
            };

            _unitOfWork.Permission.Add(permission);
            _unitOfWork.Save();

            return Ok(permission);
        }
        [HttpGet("getgroup")]
        public async Task<IActionResult> GetGroups()
        {
            var groups = await _unitOfWork.Group.GetAll();
            return Ok(groups);
        }
        [HttpPost("addgroup")]
        public async Task<IActionResult> AddGroup([FromBody] AddGroupDto groupDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var group = new Group
            {
                Name = groupDto.Name
            };

           await _unitOfWork.Group.Add(group);
            _unitOfWork.Save();

            return Ok(group);
        }
        [HttpGet("groups/{groupId}/permissions")]
        public async Task<IActionResult> GetGroupPermissions(int groupId)
        {
            var group = await _unitOfWork.Group.GetGroupWithPermissions(groupId);

            if (group == null)
            {
                return NotFound();
            }

            return Ok(group);
        }






    }
}
