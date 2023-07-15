using System.ComponentModel.DataAnnotations;

namespace AuthenticationAuthorizationProject.Dtos.PermissionDto
{
    public class AddPermissionDto
    {
        [Required]
        public string Name { get; set; }
    }
}
