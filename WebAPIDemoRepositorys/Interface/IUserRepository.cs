using WebAPIDemoRepositorys.Data;
using WebAPIDemoRepositorys.ViewModel;

namespace WebAPIDemoRepositorys.Interface
{
    public interface IUserRepository : IGenericRepository<LocalUser>
    {
        LocalUser GetUser(LoginRequestDTO loginRequestDTO);
        bool IsUniqueUser(string username);
    }
}