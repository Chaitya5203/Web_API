using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPIDemoRepositorys.ViewModel;
namespace WebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected ActionResult<APIResponse> SuccessResponse(object data, string message)
        {
            var response = new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Data = data,
                Message = message
            };
            return Ok(response);
        }
        protected ActionResult<APIResponse> ErrorResponse(string errorMessages)
        {
            var response = new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = new List<string> { errorMessages }
            };
            return BadRequest(response);
        }
    }
}