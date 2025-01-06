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
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Address> _addrssSet;

        public AddressRepository(AppDbContext context)
        {
            _context = context;
            _addrssSet = _context.Set<Address>();
        }

        public async Task SaveAsync(Address address)
        {
            await _context.Set<Address>().AddAsync(address);
          

            await _context.SaveChangesAsync();
        }


        public async Task<Address> FindByIdAsync(long id)
        {
            return await _addrssSet.FindAsync(id);
        }

        public List<Address> FindAll()
        {
            return _context.Set<Address>().ToList();
        }

        public async Task DeleteAsync(Address address)
        {
            _addrssSet.Remove(address);
            await _context.SaveChangesAsync();
        }
    }
}