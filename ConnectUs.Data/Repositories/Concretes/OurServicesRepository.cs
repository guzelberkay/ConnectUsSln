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
    public class OurServicesRepository : IOurServicesRepository
    {
        private readonly AppDbContext _context;

        public OurServicesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OurServices> SaveAsync(OurServices ourServices)
        {
            _context.OurServices.Add(ourServices);
            await _context.SaveChangesAsync();
            return ourServices;
        }

        public async Task DeleteAsync(OurServices ourServices)
        {
            _context.OurServices.Remove(ourServices);
            await _context.SaveChangesAsync();
        }

        public async Task<OurServices?> FindByIdAsync(long id)
        {
            return await _context.OurServices.FindAsync(id);
        }

        public async Task<List<OurServices>> FindAllAsync()
        {
            return await _context.OurServices.ToListAsync();
        }
    }
}
