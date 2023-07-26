using AuthenticationAuthorizationProject.Web.ViewModels;

namespace AuthenticationAuthorizationProject.Web.Services.IServices
{
    public interface IBaseServices
    {
        APIResponse responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
