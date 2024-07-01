using WebAPIDemoRepositorys.Data;
using WebAPIDemoRepositorys.ViewModel;
namespace WebAPIDemoRepositorys.Interface
{
    public interface IDemoRepository
    {
        IQueryable<Villainfo> GetVillas(int pageIndex, int pageSize);
        int GetVillaCount();
        Villainfo GetVillaById(int id);
        void UpdateVilla(Villainfo villa);
        bool GetVillaByName(string name);
        void CreateVilla(Villainfo villainfo);
        void DeleteVillaById(Villainfo villa);
    }
}
