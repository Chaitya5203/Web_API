using Microsoft.Extensions.Configuration;
using WebAPIDemoRepositorys.Interface;
using WebAPIDemoRepositorys.ViewModel;
using WebAPIDemoService.Interface;
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