using ConnectUs.Entity.Dto.request;
using ConnectUs.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Service.Services.Abstractions
{
    public interface IAuthService
    {
        Task<string> Login(LoginRequestDTO dto);  // public async string Login(LoginRequestDTO dto) olması gerekiyor
        Task<bool> LoginProfileManagement(string password, string token);
        Task<bool> ResetPassword(ResetPasswordRequestDTO dto);
   
        Task<string> FindEmailByAuthId(long authId);
        Task<bool> CheckEmailExists(string email);
        Task<Auth> GetAuthFromToken(string token);
    }
}
