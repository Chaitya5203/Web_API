using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Configuration;
using WebAPIDemoRepositorys.Data;
using WebAPIDemoRepositorys.Interface;
using WebAPIDemoRepositorys.ViewModel;
using WebAPIDemoService.Interface;
namespace WebAPIDemoService.Implementation
{
    public class DemoServices : BaseServices, IDemoServices
    {
        //private readonly IDemoRepository _repository;
        private readonly IGenericRepository<Villainfo> _repository;
        private readonly IHttpClientFactory _clientFactory;
        private string villaUrl = string.Empty;
        private readonly IMapper _mapper;
        public DemoServices(IDemoRepository repository, IMapper mapper, IHttpClientFactory clientFactory, IConfiguration configuration,IGenericRepository<Villainfo> repository1):base(clientFactory) 
        {
            _repository = repository1;
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
        public VillaDto2 GetVillas(int pageIndex, int pageSize)
        {
            var count = _repository.GetVillaCount();
            VillaDto2 villaDto = new VillaDto2();
            var villas = _repository.GetAll(pageIndex, pageSize)
                                        .Select(v => new VillaDTO
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
        public Task<T> UpdateAsync<T>(VillaDTO villaDTo)
        {
            return SendAsync<T>(new APIRequest()
            {
                Apitype = APIRequest.ApiType.PUT,
                Data = villaDTo,
                Url = villaUrl + "/api/VillaAPI/"+villaDTo.Id,
            });
        }

        public Villainfo UpdateVilla(int id, VillaDTO villaDTo)
        {
            //var villainfos= _repository.Get(id); 
            var villa = _mapper.Map<Villainfo>(villaDTo);
            _repository.Update(villa);
            if(villa == null)
            {
                throw new Exception("Villa does not exist with given id");
            }
            return villa;
        }
        public bool GetVillaByName(string name)
        {
            return _repository.GetByName(name);
        }
        public void CreateVilla(VillaDTO villaDTo)
        {
            Villainfo villainfo = _mapper.Map<Villainfo>(villaDTo);
            _repository.Insert(villainfo);
        }
        public void DeleteVilla(Villainfo villa)
        {
            _repository.Delete(villa);
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
        }
    }
}