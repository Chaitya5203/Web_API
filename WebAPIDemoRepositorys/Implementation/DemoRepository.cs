using WebAPIDemoRepositorys.Data;
using WebAPIDemoRepositorys.Interface;
namespace WebAPIDemoRepositorys.Implementation
{
    public class DemoRepository:IDemoRepository
    {
        private readonly ApplicationContext _context;
        public DemoRepository(ApplicationContext context)
        {
            _context = context;
        }
    }
}