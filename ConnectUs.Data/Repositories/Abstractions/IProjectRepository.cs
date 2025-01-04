using ConnectUs.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Data.Repositories.Abstractions
{
    public interface IProjectRepository
    {
        Task<Project> FindByIdAsync(long id);
        Task<List<Project>> FindAllAsync();
        Task SaveAsync(Project project);
        Task DeleteAsync(Project project);
    }
}

