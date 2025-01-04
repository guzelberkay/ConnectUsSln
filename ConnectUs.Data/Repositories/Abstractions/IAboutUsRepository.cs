using ConnectUs.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Data.Repositories.Abstractions
{
    public interface IAboutUsRepository
    {
        AboutUs FindFirst();
        void Save(AboutUs aboutUs);
    }
}
