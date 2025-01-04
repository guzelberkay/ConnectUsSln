using ConnectUs.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Data.Repositories.Abstractions
{
    public interface IPhoneRepository
    {
        Task SaveAsync(Phone phone);
        Task<Phone> FindByIdAsync(long id);
        Task DeleteAsync(Phone phone);
        Task UpdateAsync(Phone phone);
        Task<List<Phone>> FindAllAsync();
    }
}
