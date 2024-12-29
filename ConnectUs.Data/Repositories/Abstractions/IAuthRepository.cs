using ConnectUs.Entity.Entities;

namespace ConnectUs.Data.Repositories.Abstractions
{
    public interface IAuthRepository 
    {
        Task<Auth> FindByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
