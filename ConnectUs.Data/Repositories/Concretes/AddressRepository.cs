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

        public void Save(Address address)
        {
            if (address.Id == 0) // Yeni bir adres ise
            {
                _context.Set<Address>().Add(address);
            }
            else // Var olan bir adresi güncelle
            {
                _context.Set<Address>().Update(address);
            }

            _context.SaveChanges();
        }

        public Address? FindById(long id)
        {
            return _context.Set<Address>().FirstOrDefault(a => a.Id == id);
        }

        public List<Address> FindAll()
        {
            return _context.Set<Address>().ToList();
        }

        public void Delete(Address address)
        {
            _context.Set<Address>().Remove(address);
            _context.SaveChanges();
        }
    }
}