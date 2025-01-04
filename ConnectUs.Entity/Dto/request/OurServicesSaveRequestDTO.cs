using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Entity.Dto.request
{
    public class OurServicesSaveRequestDTO
    {
        public IFormFile Photo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Token { get; set; }
    }
}

