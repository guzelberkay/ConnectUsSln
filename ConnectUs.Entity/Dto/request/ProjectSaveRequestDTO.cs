using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Entity.Dto.request
{
    public class ProjectSaveRequestDTO
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string Employer { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string Description { get; set; }
    }
}

