using AuthenticationAuthorizationProject.Utility;
using AuthenticationAuthorizationProject.Web.Services.IServices;
using AuthenticationAuthorizationProject.Web.ViewModels;
using System.Configuration;

namespace AuthenticationAuthorizationProject.Web.Services
{
    public class RoleService : BaseServices, IRoleService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string roleUrl;
        public RoleService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            roleUrl = configuration.GetValue<string>("ServiceUrls:AuthAPI");
        }

        public Task<T> CreateAsync<T>(RoleFormViewModel dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = roleUrl + "/api/Roles/AddRole",
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token )
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = roleUrl + "/api/Roles/GetAllRoles",
                Token = token,
                
            });
        }

		public Task<T> DeleteAsync<T>(string roleId , string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Url = roleUrl + "/api/Roles/DeleteRole?roleId=" + roleId,
				Token = token
			});
		}
		public Task<T> GetAsync<T>(string roleId ,  string token)
		{
			return SendAsync<T>(new APIRequest()
             

			{
				ApiType = SD.ApiType.GET,
				Url = roleUrl + "/api/Roles/GetRoleById?roleId=" + roleId,
				Token = token
			});
		}
	}
}
