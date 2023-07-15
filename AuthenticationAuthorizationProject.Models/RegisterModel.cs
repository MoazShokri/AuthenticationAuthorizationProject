using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAuthorizationProject.Models
{
    public class RegisterModel
    {
        [Required, StringLength(100)]
        public string FristName { get; set; }
        [Required, StringLength(100)]
        public string LastName { get; set; }
        [Required, StringLength(100)]
        public string UserName { get; set; }
        [Required, StringLength(100)]
        public string Email { get; set; }
        [Required, StringLength(100)]
        public string Password { get; set; }

    }
}
