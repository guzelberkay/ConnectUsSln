using ConnectUs.Entity.Dto.request;
using ConnectUs.Entity.Dto.response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Service.Services.Abstractions
{
    public interface IOurServicesService
    {
        Task<bool> SaveAsync(OurServicesSaveRequestDTO dto);
        Task<bool> DeleteAsync(OurServicesDeleteRequestDTO dto);
        Task<byte[]> GetOneImageAsync(long id);
        Task<List<OurServicesResponseDTO>> FindAllAsync();
    }
}

