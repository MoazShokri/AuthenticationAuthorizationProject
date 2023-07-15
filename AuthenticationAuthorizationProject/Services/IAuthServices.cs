using AuthenticationAuthorizationProject.Models;
using AuthenticationAuthorizationProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAuthorizationProject.DataAccess.Services
{
    public interface IAuthServices
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModelViewModel model);
    }
}
