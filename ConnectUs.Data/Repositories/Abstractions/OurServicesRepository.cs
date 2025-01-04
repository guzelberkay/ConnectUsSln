using ConnectUs.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Data.Repositories.Abstractions
{
    public interface IOurServicesRepository
    {
        Task<OurServices> SaveAsync(OurServices ourServices);
        Task DeleteAsync(OurServices ourServices);
        Task<OurServices?> FindByIdAsync(long id);
        Task<List<OurServices>> FindAllAsync();
    }
}

