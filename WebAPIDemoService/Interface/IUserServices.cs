using WebAPIDemoRepositorys.Data;
using WebAPIDemoRepositorys.ViewModel;

namespace WebAPIDemoService.Interface
{
    public interface IUserServices
    {
        bool IsUniqueUser(string userName);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO);
    }
}
