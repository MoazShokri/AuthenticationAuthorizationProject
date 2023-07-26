using AuthenticationAuthorizationProject.Web.ViewModels;
using System.Threading.Tasks;

namespace AuthenticationAuthorizationProject.Web.Services.IServices
{
    public interface IRoleService
    {
        Task<T> GetAllAsync<T>(string token );
        //Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(RoleFormViewModel dto, string token);
       
        //Task<T> UpdateAsync<T>(VillaUpdateDTO dto, string token);
        //Task<T> DeleteAsync<T>(int id, string token);
    }
}
