using ConnectUs.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ConnectUs.Data.Repositories.Abstractions;

namespace ConnectUs.Data.Repositories.Concretes
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Auth> _authSet;

        public AuthRepository(DbContext context)
        {
            _context = context;
            _authSet = _context.Set<Auth>();
        }

        // Email ile kullanıcı bulma
        public async Task<Auth> FindByEmailAsync(string email)
        {
            return await _authSet.FirstOrDefaultAsync(auth => auth.Email == email);
        }

        // Email var mı kontrol etme
        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _authSet.AnyAsync(auth => auth.Email == email);
        }

        // authId ile kullanıcıyı bulma
        public async Task<Auth> GetByIdAsync(long authId)
        {
            return await _authSet.FirstOrDefaultAsync(auth => auth.Id == authId);
        }

        // Kullanıcıyı güncelleme
        public async Task<Auth> UpdateAsync(Auth auth)
        {
            var existingAuth = await _authSet.FindAsync(auth.Id);  // Önce var olup olmadığını kontrol et
            if (existingAuth == null)
            {
                return null;  // Eğer kullanıcı bulunmazsa null döner
            }

            // Eğer kullanıcı bulunduysa, mevcut kaydı güncelle
            _context.Entry(existingAuth).CurrentValues.SetValues(auth);
            await _context.SaveChangesAsync(); // Değişiklikleri kaydet

            return existingAuth;  // Güncellenmiş kullanıcıyı döner
        }

        // Şifre sıfırlama kodu ile kullanıcıyı bulma
        public async Task<Auth> FindByCodeAsync(string code)
        {
            return await _authSet.FirstOrDefaultAsync(auth => auth.Code == code);
        }

        // SaveAsync method to save changes to the database
        public async Task SaveAsync(Auth auth)
        {
            // We assume `auth` is an existing entity to be updated or a new entity
            if (_context.Entry(auth).State == EntityState.Detached)
            {
                _authSet.Attach(auth); // Attach the entity if it is not already being tracked
            }

            await _context.SaveChangesAsync(); // Save changes to the database
        }
    }
}
