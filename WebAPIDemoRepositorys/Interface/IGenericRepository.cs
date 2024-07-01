using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIDemoRepositorys.Data;

namespace WebAPIDemoRepositorys.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll(int pageIndex,int pageSize);
        T Get(int Id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);
        void SaveChanges();
        int GetVillaCount();
        bool GetByName(string name);
    }
}
