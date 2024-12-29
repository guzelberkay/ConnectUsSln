using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Entity.Dto.request
{
    public record ResetPasswordRequestDTO
    {
        [Required]
        public string Code { get; init; } = string.Empty;

        [Required]
        public string NewPassword { get; init; } = string.Empty;

        [Required]
        public string RePassword { get; init; } = string.Empty;
    }
}
