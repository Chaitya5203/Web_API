using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebAPIDemoRepositorys.Interface;
using WebAPIDemoRepositorys.ViewModel;
using WebAPIDemoService.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace WebAPIDemoService.Implementation
{
    public class DemoServices : BaseServices, IDemoServices
    {
        private readonly IDemoRepository _repository;
        private readonly IHttpClientFactory _clientFactory;
        private string villaUrl = string.Empty;
        public DemoServices(IDemoRepository repository, IHttpClientFactory clientFactory, IConfiguration configuration):base(clientFactory) 
        {
            _repository = repository;
            _clientFactory = clientFactory;
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
        public VillaDto2 GetVillas(int pageIndex, int pageSize)
        {
            var count = _repository.GetVillaCount();
            VillaDto2 villaDto = new VillaDto2();
            
            var villas = _repository.GetVillas(pageIndex, pageSize)
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
        //public List<VillaDTO> GetVillas(int pageIndex, int pageSize)
        //{
        //    IQueryable<VillaDTO> query;
        //    query =_repository.Getvillas(pageIndex, pageSize);
        //    var count = _repository.GetVillaCount();
        //    
        //    return new VillaDTO()
        //    {
        //        PageNumber = pageIndex,
        //        PageSize = totalPages,
        //        data=query.AsQueryable().ToList,
        //    };
        //}
        public Task<T> UpdateAsync<T>(VillaDTO villaDTo)
        {
            return SendAsync<T>(new APIRequest()
            {
                Apitype = APIRequest.ApiType.PUT,
                Data = villaDTo,
                Url = villaUrl + "/api/VillaAPI/"+villaDTo.Id,
            });
        }
    }
}