using ConnectUs.Entity.Dto.request;
using ConnectUs.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Service.Services.Abstractions
{
    public interface IPhoneService
    {
        Task<bool> SaveAsync(PhoneRequestDTO dto);
        Task<bool> DeleteAsync(string token, long id);
        Task<bool> UpdateAsync(PhoneUpdateRequestDTO dto);
        Task<List<Phone>> FindAllAsync();
    }

}