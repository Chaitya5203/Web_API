using WebAPIDemoRepositorys.ViewModel;
namespace WebAPIDemoService.Interface
{
    public interface IBaseServices
    {
        APIResponse responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}