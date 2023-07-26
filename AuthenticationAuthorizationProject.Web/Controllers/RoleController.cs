using AuthenticationAuthorizationProject.Constants;
using AuthenticationAuthorizationProject.Utility;
using AuthenticationAuthorizationProject.Web.Services.IServices;
using AuthenticationAuthorizationProject.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuthenticationAuthorizationProject.Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
     

        public RoleController(IRoleService roleService  )
        {
            this._roleService = roleService;
           
        }
        [Authorize(Permissions.Factory.View)]
        public async Task<IActionResult> IndexRole(string roleId)
        {

            List<ListOfRole> list = new();

            var response = await _roleService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ListOfRole>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult> AddRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RoleFormViewModel model)
        {
            if (ModelState.IsValid)
            {

                var response = await _roleService.CreateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Role created successfully";
                    return RedirectToAction(nameof(IndexRole));
                }
            }
            TempData["error"] = "Error encountered.";
            return View(model);
        }
    }
    
}
