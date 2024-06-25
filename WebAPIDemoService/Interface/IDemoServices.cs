using WebAPIDemoRepositorys.ViewModel;

namespace WebAPIDemoService.Interface
{
    public interface IDemoServices
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(VillaDTO villaDTo);
        Task<T> UpdateAsync<T>(VillaDTO villaDTo);
        Task<T> DeleteAsync<T>(int id);
    }
}
