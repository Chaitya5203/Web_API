using Microsoft.EntityFrameworkCore;
using WebAPIDemoRepositorys.Data;
using WebAPIDemoRepositorys.Interface;

namespace WebAPIDemoRepositorys.Implementation
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class
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
            _context.SaveChanges();
        }
        public T Get(int Id)
        {
            return entities.Find(Id);
        }
        public IEnumerable<T> GetAll(int pageIndex,int pageSize)
        {
            return entities.AsEnumerable().Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize); 
        }
        public bool GetByName(string name)
        {
            return _context.Villainfos.Select(e => e.Name).Contains(name);
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
            _context.SaveChanges();
        }

        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            _context.SaveChanges();
        }
    }
}
