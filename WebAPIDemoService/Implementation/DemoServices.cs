using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebAPIDemoRepositorys.Data;
using WebAPIDemoRepositorys.Interface;
using WebAPIDemoRepositorys.ViewModel;
using WebAPIDemoService.Interface;
namespace WebAPIDemoService.Implementation
{
    public class DemoServices : BaseServices, IDemoServices
    {
        private readonly IDemoRepository _repository;
        //private readonly IGenericRepository<Villainfo> _repository;
        private readonly IHttpClientFactory _clientFactory;
        private string villaUrl = string.Empty;
        private readonly IMapper _mapper;
        public DemoServices(IMapper mapper,IDemoRepository repository, IHttpClientFactory clientFactory, IConfiguration configuration):base(clientFactory) 
        {
            _repository = repository;
            _clientFactory = clientFactory;
            _mapper = mapper;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI")!;
        }     
        public Task<T> CreateAsync<T>(VillaDTO villaDTo)
        {
            return SendAsync<T>(new APIRequest()
            {
                Apitype = APIRequest.ApiType.POST,
                Data = villaDTo,
                Url = villaUrl + "/api/VillaAPI",
            });
        }
        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                Apitype = APIRequest.ApiType.DELETE,
                Url = villaUrl + "/api/VillaAPI/"+id,
            });
        }
        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                Apitype = APIRequest.ApiType.GET,
                Url = villaUrl + "/api/VillaAPI",
            });
        }
        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                Apitype = APIRequest.ApiType.GET,
                Url = villaUrl + "/api/VillaAPI/"+id,
            });
        }
        public Villainfo getvillabyid(int id)
        {
            return _repository.Get(id);
        }
        public async Task<VillaDto2> GetVillas(int pageIndex, int pageSize, string search = null)
        {
            var villaDto = new VillaDto2();
            if (!string.IsNullOrEmpty(search))
            {
                var filteredVillas = await SearchVillas(search, pageIndex, pageSize);
                return filteredVillas;
            }
            else
            {
                var allVillas = GetAllVillas(pageIndex, pageSize);
                return allVillas;
            }
        }
        private VillaDto2 GetAllVillas(int pageIndex, int pageSize)
        {
            var count = _repository.GetVillaCount();
            VillaDto2 villaDto = new VillaDto2();
            var villas = _repository.GetAll(pageIndex, pageSize)
                                        .Select(v => new GetAllVillaDTO
                                        {
                                            Id = v.Id,
                                            Name = v.Name,
                                            Amenity = v.Amenity,
                                            Details = v.Details,    
                                            Rate = (decimal)v.Rate,
                                            sqft = (int)v.Sqft,
                                            occupancy = (int)v.Occupancy,
                                        })
                                        .ToList();
            villaDto.data = villas;
            villaDto.CurrentPage = pageIndex;
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);
            villaDto.NextPage = totalPages != pageIndex;
            villaDto.PreviousPage = pageIndex != 1;
            villaDto.TotalPages = count;
            return villaDto;
        }
        private async Task<VillaDto2> SearchVillas(string search, int pageIndex, int pageSize)
        {
            var count = await _repository.CountByName(search);
            VillaDto2 villaDto = new VillaDto2();
            var filteredVillas = await _repository.Search(search, pageIndex, pageSize)
                                                 .Select(v => new GetAllVillaDTO
                                                 {
                                                     Id = v.Id,
                                                     Name = v.Name,
                                                     Amenity = v.Amenity,
                                                     Details = v.Details,
                                                     Rate = (decimal)v.Rate,
                                                     sqft = (int)v.Sqft,
                                                     occupancy = (int)v.Occupancy,
                                                 })
                                                 .ToListAsync();
            villaDto.data = filteredVillas;
            villaDto.CurrentPage = pageIndex;
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);
            villaDto.NextPage = totalPages != pageIndex;
            villaDto.PreviousPage = pageIndex != 1;
            villaDto.TotalPages = count;
            return villaDto;
        }
        //public Task<T> UpdateAsync<T>(VillaDTO villaDTo)
        //{
        //    return SendAsync<T>(new APIRequest()
        //    {
        //        Apitype = APIRequest.ApiType.PUT,
        //        Data = villaDTo,
        //        Url = villaUrl + "/api/VillaAPI/"+villaDTo.Id,
        //    });
        //}

        public Villainfo UpdateVilla(int id, VillaDTO villaDTo)
        {
            var villainfos = _repository.Get(id);
            if (villainfos == null)
            {
                throw new Exception("Villa does not exist with given id");
            }
            villainfos = _mapper.Map(villaDTo, villainfos);
            _repository.Update(villainfos);
            _repository.Save();            
            return villainfos;
        }
        public bool GetVillaByName(string name)
        {
            return _repository.GetByName(name);
        }
        public void CreateVilla(VillaDTO villaDTo)
        {
            Villainfo villainfo = _mapper.Map<Villainfo>(villaDTo);
            villainfo.Createddate = DateTime.Now;
            _repository.Insert(villainfo);
            _repository.Save();
        }
        public void DeleteVilla(Villainfo villa)
        {
            _repository.Delete(villa);
            _repository.Save();
        }
        public void UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            var villa = _repository.Get(id);
            if (villa == null)
            {
                throw new Exception($"Villa with id {id} not found.");
            }
            VillaDTO villaDTO = _mapper.Map<VillaDTO>(patchDTO);               
            patchDTO.ApplyTo(villaDTO);
            var updateVilla = new Villainfo()
            {
                Id = id,
                Name = villaDTO.Name,
                Sqft = villaDTO.sqft,
                Occupancy = villaDTO.occupancy,
                Rate = villaDTO.Rate,
                Details = villaDTO.Details,
                Amenity = villaDTO.Amenity,
            };
            _repository.Update(updateVilla);
            _repository.Save();
        }
    }
}