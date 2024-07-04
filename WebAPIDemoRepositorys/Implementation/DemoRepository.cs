using WebAPIDemoRepositorys.Data;
using WebAPIDemoRepositorys.Interface;
namespace WebAPIDemoRepositorys.Implementation
{
    public class DemoRepository : GenericRepository<Villainfo>, IDemoRepository
    {
        private readonly ApplicationContext _context;
        public DemoRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
        //public IQueryable<Villainfo> GetVillas(int pageIndex, int pageSize)
        //{
        //    return _context.Villainfos
        //                    .Skip((pageIndex - 1) * pageSize)
        //                    .Take(pageSize);
        //}
        //public int GetVillaCount()
        //{
        //    return _context.Villainfos.Count();
        //}
        //public Villainfo GetVillaById(int id)
        //{
        //    return _context.Villainfos.AsNoTracking().FirstOrDefault(e => e.Id == id);
        //}
        //public void UpdateVilla(Villainfo villa)
        //{
        //    _context.Villainfos.Update(villa);
        //    _context.SaveChanges();
        //}
        //public bool GetVillaByName(string name)
        //{
        //    return _context.Villainfos.Select(e => e.Name).Contains(name);
        //}
        //public void CreateVilla(Villainfo villainfo)
        //{
        //    _context.Villainfos.Add(villainfo);
        //    _context.SaveChanges();
        //}
        //public void DeleteVillaById(Villainfo villa)
        //{
        //    _context.Villainfos.Remove(villa);
        //    _context.SaveChanges();
        //}
    }
}