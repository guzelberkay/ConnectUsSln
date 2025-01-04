using ConnectUs.Entity.Entities;
using ConnectUs.Entity.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Data.Repositories.Abstractions
{
    public interface ICommentRepository
    {
        Task<List<Comment>> FindByProjectIdAndStatusAsync(long projectId, EStatus status);
        Task<List<Comment>> FindByProjectIdAsync(long projectId);
        Task<Comment> FindByIdAsync(long commentId);
        Task SaveAsync(Comment comment);
        Task DeleteAsync(Comment comment);
        Task<List<Comment>> GetAllAsync();
    }
}
