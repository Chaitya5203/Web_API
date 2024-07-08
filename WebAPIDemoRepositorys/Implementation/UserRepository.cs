using WebAPIDemoRepositorys.Data;
using WebAPIDemoRepositorys.Interface;
using WebAPIDemoRepositorys.ViewModel;
namespace WebAPIDemoRepositorys.Implementation
{
    public class UserRepository : GenericRepository<LocalUser>, IUserRepository
    {
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context) : base(context)
        {
            _context = context;           
        }

        public LocalUser GetUser(LoginRequestDTO loginRequestDTO)
        {
            return _context.LocalUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower() && u.Password == loginRequestDTO.Password);
        }

        public bool IsUniqueUser(string username)
        {
            var user = _context.LocalUsers.FirstOrDefault(x=>x.UserName.ToLower() == username.ToLower());
            if(user == null)
            {
                return false;
            }
            else
            {
                return true; 
            }    
        }        
    }
}