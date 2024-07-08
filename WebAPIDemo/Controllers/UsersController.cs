using Microsoft.AspNetCore.Mvc;
using WebAPIDemoRepositorys.ViewModel;
using WebAPIDemoService.Interface;
namespace WebAPIDemo.Controllers
{
    [Route("api/UserAuth")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserServices _service;
        public UsersController(IUserServices userServices)
        {
            _service = userServices;
        }
        [HttpPost("login")]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var LoginResponse = await _service.Login(loginRequestDTO);
            if (LoginResponse.User == null || string.IsNullOrEmpty(LoginResponse.Token))
            {
                return ErrorResponse("Username pr password is incorrect");
            }
            else
            {
                return SuccessResponse(LoginResponse, "Login Successfully");
            }
            
        }
        [HttpPost("register")]
        public async Task<ActionResult<APIResponse>> Register([FromBody] RegistrationRequestDTO registrationRequestDTO)
        {
            bool Isuserexist = _service.IsUniqueUser(registrationRequestDTO.UserName);
            var RegisterResponse = await _service.Register(registrationRequestDTO);
            if (RegisterResponse == null)
            {
                return ErrorResponse("Username pr password is incorrect");
            }
            else
            {
                return SuccessResponse(RegisterResponse, "Login Successfully");
            }

        }
    }
}
