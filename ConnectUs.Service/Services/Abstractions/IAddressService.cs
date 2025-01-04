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
        bool Save(AddressRequestDTO dto);
        bool Delete(string token, long id);
        bool Update(AddressUpdateRequestDTO dto);
        List<Address> FindAll();
    }
}
