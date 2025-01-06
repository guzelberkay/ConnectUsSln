using ConnectUs.Entity.Dto.request;
using ConnectUs.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Service.Services.Abstractions
{
    public interface IAddressService
    {
        Task<bool> SaveAsync(AddressRequestDTO dto);

        Task<bool> DeleteAsync(string token, long id);
        List<Address> FindAll();
    }
}
