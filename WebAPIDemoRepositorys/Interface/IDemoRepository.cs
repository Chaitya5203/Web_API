using WebAPIDemoRepositorys.Data;
using WebAPIDemoRepositorys.ViewModel;

namespace WebAPIDemoRepositorys.Interface
{
    public interface IDemoRepository
    {
        IQueryable<Villainfo> GetVillas(int pageIndex, int pageSize);
        int GetVillaCount();
    }
}
