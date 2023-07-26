﻿using System.ComponentModel.DataAnnotations;

namespace AuthenticationAuthorizationProject.Web.ViewModels
{
    public class AddRoleModelViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
