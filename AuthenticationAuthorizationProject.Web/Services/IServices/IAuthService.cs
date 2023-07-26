using AuthenticationAuthorizationProject.Models;

namespace AuthenticationAuthorizationProject.Web.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(TokenRequestModel objToCreate);
        Task<T> RegisterAsync<T>(RegisterModel objToCreate);
    }
}
