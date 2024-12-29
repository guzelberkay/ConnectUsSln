using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Entity.Dto.request
{
    public record ChangeMyPasswordRequestDTO
    {
        [Required]
        public long AuthId { get; init; }

        [Required]
        public string NewPassword { get; init; } = string.Empty;

        [Required]
        public string NewConfirmPassword { get; init; } = string.Empty;
    }
}