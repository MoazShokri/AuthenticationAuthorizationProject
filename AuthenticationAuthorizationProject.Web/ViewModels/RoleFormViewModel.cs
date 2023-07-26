using System.ComponentModel.DataAnnotations;

namespace AuthenticationAuthorizationProject.Web.ViewModels
{
    public class RoleFormViewModel
    {
        [Required, StringLength(256)]
        public string Name { get; set; }
    }
}
