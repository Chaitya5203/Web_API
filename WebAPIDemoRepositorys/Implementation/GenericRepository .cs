using Microsoft.EntityFrameworkCore;
using WebAPIDemoRepositorys.Data;
using WebAPIDemoRepositorys.Interface;
namespace WebAPIDemoRepositorys.Implementation
{
    public class GenericRepository<T>: IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext _context;
        private DbSet<T> entities;
        public GenericRepository(ApplicationContext context)
        {
            _context = context;
            entities = _context.Set<T>();
        }
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }
        public T Get(int Id)
        {
            return entities.AsNoTracking().SingleOrDefault(e=>e.Id==Id);
        }
        public IEnumerable<T> GetAll(int pageIndex,int pageSize)
        {
            return entities.AsEnumerable().Skip((pageIndex - 1) * pageSize).Take(pageSize); 
        }
        public bool GetByName(string name)
        {
            return entities.Select(e => e.Name).Contains(name);
        }
        public int GetVillaCount()
        {
            return entities.Count();
        }
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
        }
        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public IQueryable<T> Search(string name, int pageIndex, int pageSize)
        {
            return entities.Where(v => v.Name.Contains(name)).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        public async Task<int> CountByName(string name)
        {
            return await entities.Where(v => v.Name.Contains(name)).CountAsync();
        }
        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
        }
        //public async Task<IEnumerable<T>> Search(int pageIndex, int pageSize, string name)
        //{
        //    IQueryable<T> query = entities;
        //    if(!string.IsNullOrEmpty(name))
        //    {
        //        query = query.Where(a=>a.Name.Contains(name));
        //    }
        //    return await query.Skip((pageIndex - 1) * pageSize)
        //                    .Take(pageSize).ToListAsync();
        //}
    }
}