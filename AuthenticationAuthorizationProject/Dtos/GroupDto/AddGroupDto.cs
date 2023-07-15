using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationAuthorizationProject.Dtos.GroupDto
{
    public class AddGroupDto
    {
        [Required]
        public string Name { get; set; }
    }
}
