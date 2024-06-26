using Microsoft.EntityFrameworkCore;
using System.Numerics;
using WebAPIDemoRepositorys.Data;
using WebAPIDemoRepositorys.Interface;
using WebAPIDemoRepositorys.ViewModel;
namespace WebAPIDemoRepositorys.Implementation
{
    public class DemoRepository : IDemoRepository
    {
        private readonly ApplicationContext _context;
        public DemoRepository(ApplicationContext context)
        {
            _context = context;
        }
        public IQueryable<Villainfo> GetVillas(int pageIndex, int pageSize)
        {
            return _context.Villainfos
                            .Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize);
        }
        public int GetVillaCount()
        {
            return _context.Villainfos.Count();
        }
    }
}