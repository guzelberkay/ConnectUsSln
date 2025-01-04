using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Entity.Dto.request
{
    public record ApproveCommentRequestDTO
    {
        [Required]
        public string Token { get; init; }

        [Required]
        public long Id { get; init; }
    }
}
