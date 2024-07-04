using Microsoft.AspNetCore.JsonPatch;
using WebAPIDemoRepositorys.Data;
using WebAPIDemoRepositorys.ViewModel;
namespace WebAPIDemoService.Interface
{
    public interface IDemoServices
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(VillaDTO villaDTo);
        Task<T> DeleteAsync<T>(int id);
        //Task<T> UpdateAsync<T>(VillaDTO villaDTo);
        //VillaDto2 GetVillas(int pageIndex, int pageSize);
        //Task<IEnumerable<Villainfo>> Search(int pageIndex, int pageSize,string name);
        Villainfo getvillabyid(int id);
        Villainfo UpdateVilla(int id, VillaDTO villaDTo);
        bool GetVillaByName(string? name);
        void CreateVilla(VillaDTO villaDTo);
        void DeleteVilla(Villainfo villa);
        void UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO);
        Task<VillaDto2> GetVillas(int pageIndex, int pageSize, string search = null);
    }
}