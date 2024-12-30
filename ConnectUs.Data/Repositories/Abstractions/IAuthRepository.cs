using ConnectUs.Entity.Entities;

namespace ConnectUs.Data.Repositories.Abstractions
{
    public interface IAuthRepository
    {
        Task<Auth> FindByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email);
        Task<Auth> FindByIdAsync(long authId);
        Task<Auth> FindByCodeAsync(string code);
        Task<Auth> UpdateAsync(Auth auth);
        Task SaveAsync(Auth auth);
    }

}
