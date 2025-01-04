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
        void Save(Address address); // Yeni bir adres kaydet
        Address? FindById(long id); // Adresi ID'ye göre bul
        List<Address> FindAll(); // Tüm adresleri listele
        void Delete(Address address); // Adresi sil
    }
}
