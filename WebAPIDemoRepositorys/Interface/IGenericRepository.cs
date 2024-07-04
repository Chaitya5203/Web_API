namespace WebAPIDemoRepositorys.Interface
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll(int pageIndex,int pageSize);
        T Get(int Id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);
        void Save();
        int GetVillaCount();
        bool GetByName(string name);
        Task<int> CountByName(string name);
        //Task<IEnumerable<T>> Search(int pageIndex, int pageSize, string name);
        IQueryable<T> Search(string name, int pageIndex, int pageSize);
    }
}