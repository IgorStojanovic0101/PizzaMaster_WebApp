using PizzaMaster.Application.Services;
using PizzaMaster.Infrastructure.System;
using PizzaMaster.Shared.DTOs.Admin;
using PizzaMaster.Shared.DTOs.User;
using PizzaMaster.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMaster.BusinessLogic.Services
{
    public class AdminService : IAdminService
    {
        private readonly RestClient _restClient;

        public AdminService(RestClient restClient)
        {
            _restClient = restClient;
        }

        public ServiceResponse<Admin_ResponseDTO> AddVideo(Admin_RequestDTO request)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<Admin_ResponseDTO> SetAdminData(Admin_RequestDTO request)
        {
           return  _restClient.wsPost<ServiceResponse<Admin_ResponseDTO>, Admin_RequestDTO>(SystemUrls.Admin.SetAdminData, request, true);
        }
    }
}
