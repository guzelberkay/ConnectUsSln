using ConnectUs.Data.Context;
using ConnectUs.Data.Repositories.Abstractions;
using ConnectUs.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Data.Repositories.Concretes
{
    public class AboutUsRepository : IAboutUsRepository
    {
        private readonly AppDbContext _context;

        public AboutUsRepository(AppDbContext context)
        {
            _context = context;
        }

        public AboutUs FindFirst()
        {
            return _context.AboutUs.OrderBy(a => a.Id).FirstOrDefault();
        }

        public void Save(AboutUs aboutUs)
        {
            if (aboutUs.Id == 0)
            {
                _context.AboutUs.Add(aboutUs);
            }
            else
            {
                _context.AboutUs.Update(aboutUs);
            }

            _context.SaveChanges();
        }
    }
}