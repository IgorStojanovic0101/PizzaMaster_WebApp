using Microsoft.AspNetCore.Mvc;
using PizzaMaster.Shared.DTOs.Admin;
using PizzaMaster.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMaster.Application.Services
{
    public interface IAdminService
    {
        ServiceResponse<Admin_ResponseDTO> SetAdminData(Admin_RequestDTO request);
        ServiceResponse<Admin_ResponseDTO> AddVideo(Admin_RequestDTO request);
    }
}
