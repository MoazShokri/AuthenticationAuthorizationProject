using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationAuthorizationProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(50)]
        public string FristName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
    }
}
