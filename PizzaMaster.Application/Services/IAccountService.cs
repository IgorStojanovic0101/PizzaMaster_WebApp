using PizzaMaster.Shared.DTOs.User;
using PizzaMaster.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMaster.Application.Services
{
    public interface IAccountService
    {
         ServiceResponse<UserLoginResponseDTO> Login(UserLoginRequestDTO dto);

         bool StoreToken(UserLoginResponseDTO responseDTO);
         string GetRole(UserLoginResponseDTO responseDto);

    }
}
