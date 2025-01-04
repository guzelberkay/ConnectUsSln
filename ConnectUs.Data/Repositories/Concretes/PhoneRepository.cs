using ConnectUs.Data.Context;
using ConnectUs.Data.Repositories.Abstractions;
using ConnectUs.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Data.Repositories.Concretes
{
    public class PhoneRepository : IPhoneRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Phone> _phoneSet;

        public PhoneRepository(AppDbContext context)
        {
            _context = context;
            _phoneSet = _context.Set<Phone>();
        }

        public async Task SaveAsync(Phone phone)
        {
            await _phoneSet.AddAsync(phone);  // AddAsync doğru DbSet üzerinde yapılmalı
            await _context.SaveChangesAsync();  // Kaydetme işlemi yapılmalı
        }

        public async Task<Phone> FindByIdAsync(long id)
        {
            return await _phoneSet.FindAsync(id);
        }

        public async Task DeleteAsync(Phone phone)
        {
            _phoneSet.Remove(phone);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Phone phone)
        {
            _phoneSet.Update(phone);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Phone>> FindAllAsync()
        {
            return await _phoneSet.ToListAsync();
        }
    }
}
