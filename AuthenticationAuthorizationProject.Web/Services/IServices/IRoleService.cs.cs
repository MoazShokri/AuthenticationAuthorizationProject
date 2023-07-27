using AuthenticationAuthorizationProject.Web.ViewModels;
using System.Threading.Tasks;

namespace AuthenticationAuthorizationProject.Web.Services.IServices
{
    public interface IRoleService
    {
        Task<T> GetAllAsync<T>(string token );
        //Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(RoleFormViewModel dto, string token);
        Task<T> DeleteAsync<T>(string roleId , string token);
        Task<T> GetAsync<T>(string roleId , string token);

	}
}
