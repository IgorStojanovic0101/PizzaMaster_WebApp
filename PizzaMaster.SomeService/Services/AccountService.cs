using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using PizzaMaster.Application.Services;
using PizzaMaster.Infrastructure.System;
using PizzaMaster.Shared.DTOs.User;
using PizzaMaster.Shared.Results;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMaster.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly RestClient _restClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(RestClient restClient, IHttpContextAccessor httpContextAccessor)
        {
            this._restClient = restClient;
            this._httpContextAccessor = httpContextAccessor;
        }
        public ServiceResponse<UserLoginResponseDTO> Login(UserLoginRequestDTO dto) => _restClient.wsPost<ServiceResponse<UserLoginResponseDTO>, UserLoginRequestDTO>(SystemUrls.User.Login, dto); 
        
        
        

        public bool StoreToken(UserLoginResponseDTO responseDto)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(responseDto.Token) as JwtSecurityToken;

            if (jsonToken != null)
            {
                var expirationClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);
 
                var expirationDateTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expirationClaim?.Value)).UtcDateTime;

                if (expirationDateTime < DateTime.UtcNow)
                {
                    return false;
                }

                this._httpContextAccessor.HttpContext.Session.Set("Token",Encoding.UTF8.GetBytes(responseDto.Token));


                return true;
                
            }
            return false;
        }

        public string GetRole(UserLoginResponseDTO responseDto)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(responseDto.Token) as JwtSecurityToken;

            
            var roleClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

              
            return roleClaim.Value;
         
        }
    }
}
