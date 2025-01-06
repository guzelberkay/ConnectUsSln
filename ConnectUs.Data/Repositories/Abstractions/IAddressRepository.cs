using ConnectUs.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Data.Repositories.Abstractions
{
    public interface IAddressRepository
    {
        Task SaveAsync(Address address); // Yeni bir adres kaydet
        Task<Address> FindByIdAsync(long id);
        List<Address> FindAll(); // Tüm adresleri listele
        Task DeleteAsync(Address address);

    }
}
