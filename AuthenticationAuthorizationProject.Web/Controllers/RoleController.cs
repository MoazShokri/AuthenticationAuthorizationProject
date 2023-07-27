
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
        public async Task<IActionResult> IndexRole()
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
        [HttpGet]
		public async Task<IActionResult> DeleteRole(string roleId)
        {
            var response = await _roleService.GetAsync<APIResponse>(roleId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                ListOfRole model = JsonConvert.DeserializeObject<ListOfRole>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
		public async Task<IActionResult> DeleteRole(ListOfRole model)
        {

            var response = await _roleService.DeleteAsync<APIResponse>(model.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa deleted successfully";
                return RedirectToAction(nameof(IndexRole));
            }
            TempData["error"] = "Error encountered.";
            return View(model);
        }

    }
    
}
