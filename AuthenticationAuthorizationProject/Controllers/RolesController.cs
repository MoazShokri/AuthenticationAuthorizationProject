using AuthenticationAuthorizationProject.Constants;
using AuthenticationAuthorizationProject.DataAccess.Repository.IRepository;
using AuthenticationAuthorizationProject.Model;
using AuthenticationAuthorizationProject.Models;
using AuthenticationAuthorizationProject.ViewModels;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
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
        protected APIResponse _response;


        public RolesController(RoleManager<IdentityRole> roleManager , UserManager<ApplicationUser> userManager , IUnitOfWork unitOfWork)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._unitOfWork = unitOfWork;
            _response = new();
        }
      
		[HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var Roles = await _roleManager.Roles.ToListAsync();
            if(Roles == null || Roles.Count == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Role is not  exist");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = Roles;
            return Ok(_response);
           
        }
		[HttpGet("GetRoleById")]
		public async Task<ActionResult<APIResponse>> GetRoleById(string roleId)
		{
			var role = await _roleManager.FindByIdAsync(roleId);
            try {
				if (role == null)
				{
					_response.StatusCode = HttpStatusCode.NotFound;
					_response.IsSuccess = false;
					_response.ErrorMessages.Add("Role not found");
					return NotFound(_response);

				}
				var roleViewModel = new RoleFormViewModel
				{

					Name = role.Name
				};

				_response.StatusCode = HttpStatusCode.OK;
				_response.IsSuccess = true;
				_response.Result = roleViewModel;
				return Ok(_response);
			}
            catch(Exception ex)
            {

				_response.IsSuccess = false;
				_response.ErrorMessages
					 = new List<string>() { ex.ToString() };
			}

            return _response;

		}
	
	[HttpPost("AddRole")]
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
		[HttpDelete("DeleteRole")]
		public async Task<ActionResult<APIResponse>> DeleteRole(string roleId)
		{
			var role = await _roleManager.FindByIdAsync(roleId);
			if (role == null)
			{
				_response.StatusCode = HttpStatusCode.NotFound;
				_response.IsSuccess = false;
				_response.ErrorMessages.Add("Role not found");
				return NotFound(_response);
			}

			// Check if there are any users in this role
			var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
			if (usersInRole.Count > 0)
			{
				_response.StatusCode = HttpStatusCode.NotFound;
				_response.IsSuccess = false;
				_response.ErrorMessages.Add(" dont can delete role ");
				return NotFound(_response);
			}

			// Delete the role
			var result = await _roleManager.DeleteAsync(role);
			if (!result.Succeeded)
			{
				_response.StatusCode = HttpStatusCode.NotFound;
				_response.IsSuccess = false;
				_response.ErrorMessages.Add(" dont can delete role ");
				return NotFound(_response);
			}
			_response.StatusCode = HttpStatusCode.OK;
			_response.IsSuccess = true;
			_response.Result = result;
			return Ok(_response);

		}

		// TODO : Permission to Roles
		[HttpGet("GetPermissionsToRoleId")]
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
        [HttpPost("AddPermissions/Claims")]
        public async Task<IActionResult> ManagePermissions(PermissionsFormViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);

            if (role == null)
                return NotFound();

            var roleClaims = await _roleManager.GetClaimsAsync(role);
            //TODO : Select CheckBox 

            //foreach (var claim in roleClaims)
            //    await _roleManager.RemoveClaimAsync(role, claim);

            var selectedClaims = model.RoleCalims.Where(c => c.IsSelected).ToList();

            foreach (var claim in selectedClaims)
                await _roleManager.AddClaimAsync(role, new Claim("Permissions", claim.DisplayValue));

            return Ok(model);
        }






    }
}
