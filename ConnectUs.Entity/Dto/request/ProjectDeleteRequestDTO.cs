using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Entity.Dto.request
{
    public class ProjectDeleteRequestDTO
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public long ProjectId { get; set; }
    }
}

