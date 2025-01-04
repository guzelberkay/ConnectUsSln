using ConnectUs.Entity.Dto.request;
using ConnectUs.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Service.Services.Abstractions
{
    public interface IProjectService
    {
        Task<bool> SaveAsync(ProjectSaveRequestDTO dto);
        Task<bool> DeleteAsync(ProjectDeleteRequestDTO dto);
        Task<List<Project>> FindAllAsync();
        Task<Project> FindProjectByIdAsync(long projectId);
    }
}
