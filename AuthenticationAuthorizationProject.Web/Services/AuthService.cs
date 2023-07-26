using AuthenticationAuthorizationProject.Models;
using AuthenticationAuthorizationProject.Utility;
using AuthenticationAuthorizationProject.Web.Services.IServices;
using AuthenticationAuthorizationProject.Web.ViewModels;

namespace AuthenticationAuthorizationProject.Web.Services
{
    public class AuthService : BaseServices, IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string AuthUrl;

        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            AuthUrl = configuration.GetValue<string>("ServiceUrls:AuthAPI");

        }

        public Task<T> LoginAsync<T>(TokenRequestModel obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = AuthUrl + "/api/Auth/token"
            });
        }

        public Task<T> RegisterAsync<T>(RegisterModel obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = AuthUrl + "/api/Auth/register"
            });
        }

    }
}
