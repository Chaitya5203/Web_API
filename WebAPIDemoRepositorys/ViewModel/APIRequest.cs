namespace WebAPIDemoRepositorys.ViewModel
{
    public class APIRequest
    {
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
        public ApiType Apitype { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
    }
}
