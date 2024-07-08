using WebAPIDemoRepositorys.Data;

namespace WebAPIDemoRepositorys.ViewModel
{
    public class LoginResponseDTO
    {
        public LocalUser User { get; set; }
        public string Token { get; set; }
    }
}
