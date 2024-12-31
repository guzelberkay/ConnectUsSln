using ConnectUs.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ConnectUs.Data.Repositories.Abstractions;
using ConnectUs.Data.Context;

namespace ConnectUs.Data.Repositories.Concretes
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Auth> _authSet;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
            _authSet = _context.Set<Auth>();
        }

        public async Task<Auth> FindByEmailAsync(string email)
        {
            return await _authSet.FirstOrDefaultAsync(auth => auth.Email == email);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _authSet.AnyAsync(auth => auth.Email == email);
        }

        public async Task<Auth> FindByIdAsync(long authId)
        {
            return await _authSet.FindAsync(authId);
        }

        public async Task<Auth> FindByCodeAsync(string code)
        {
            return await _authSet.FirstOrDefaultAsync(auth => auth.Code == code);
        }

        public async Task<Auth> UpdateAsync(Auth auth)
        {
            var existingAuth = await _authSet.FindAsync(auth.Id);
            if (existingAuth == null)
            {
                return null;
            }

            _context.Entry(existingAuth).CurrentValues.SetValues(auth);
            await _context.SaveChangesAsync();

            return existingAuth;
        }

        public async Task SaveAsync(Auth auth)
        {
            if (_context.Entry(auth).State == EntityState.Detached)
            {
                _authSet.Attach(auth);
            }

            await _context.SaveChangesAsync();
        }
    }
}
