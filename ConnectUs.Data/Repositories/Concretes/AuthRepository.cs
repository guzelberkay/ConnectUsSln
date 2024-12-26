using ConnectUs.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ConnectUs.Data.Repositories.Abstractions;

namespace ConnectUs.Data.Repositories.Concretes
{
    public interface IAuthRepository : IRepository<Auth>
    {
        Task<Auth> FindByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email);
    }

    public class AuthRepository : Repository<Auth>, IAuthRepository
    {
        public AuthRepository(DbContext context) : base(context) { }

        // E-posta ile Auth objesini bulma
        public async Task<Auth> FindByEmailAsync(string email)
        {
            return await _context.Set<Auth>().FirstOrDefaultAsync(a => a.Email == email);
        }

        // E-posta ile Auth objesinin varlığını kontrol etme
        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Set<Auth>().AnyAsync(a => a.Email == email);
        }
    }
}
