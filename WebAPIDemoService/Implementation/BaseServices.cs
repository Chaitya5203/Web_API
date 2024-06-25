using Newtonsoft.Json;
using System.Text;
using WebAPIDemoRepositorys.ViewModel;
using WebAPIDemoService.Interface;
namespace WebAPIDemoService.Implementation
{
    public class BaseServices : IBaseServices
    {
        public APIResponse responseModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }
        public BaseServices(IHttpClientFactory httpClient)
        {
            this.responseModel = new();
            this.httpClient = httpClient;
        }
        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            var client = httpClient.CreateClient("MagicAPI");
            HttpRequestMessage message = new HttpRequestMessage();
            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri(apiRequest.Url);
            if (apiRequest.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
            }
            switch (apiRequest.Apitype)
            {
                case APIRequest.ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case APIRequest.ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                case APIRequest.ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }
            HttpResponseMessage apiResponse = null;
            apiResponse = await client.SendAsync(message);
            var apiContent = await apiResponse.Content.ReadAsStringAsync();
            var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
            return APIResponse;
        }
    }
}