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
        Task<T> UpdateAsync<T>(VillaDTO villaDTo);
        Task<T> DeleteAsync<T>(int id);
        VillaDto2 GetVillas(int pageIndex, int pageSize);
        Villainfo getvillabyid(int id);
        Villainfo UpdateVilla(int id, VillaDTO villaDTo);
        bool GetVillaByName(string? name);
        void CreateVilla(VillaDTO villaDTo);
        void DeleteVilla(Villainfo villa);
        void UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO);
    }
}
