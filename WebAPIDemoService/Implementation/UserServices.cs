using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPIDemoRepositorys.Data;
using WebAPIDemoRepositorys.Interface;
using WebAPIDemoRepositorys.ViewModel;
using WebAPIDemoService.Interface;

namespace WebAPIDemoService.Implementation
{
    public class UserServices:IUserServices
    {
        private string secretkey;
        private IUserRepository _repository;
        public UserServices(IUserRepository userRepository, IConfiguration configuration)
        {
            _repository = userRepository;
            secretkey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public bool IsUniqueUser(string userName)
        {
            return _repository.IsUniqueUser(userName);
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _repository.GetUser(loginRequestDTO);
            if (user == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }
            //If User Found Generate JWT Token 
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretkey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenhandler.WriteToken(token),
                User = user
            };
            return loginResponseDTO;
        }
        public async Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            LocalUser user = new()
            {
                UserName = registrationRequestDTO.UserName,
                Password = registrationRequestDTO.Password,
                Role = registrationRequestDTO.Role,
                Name = registrationRequestDTO.Name
            };
            _repository.Insert(user);
            _repository.Save();
            user.Password = "";
            return user;
        }
    }
}
