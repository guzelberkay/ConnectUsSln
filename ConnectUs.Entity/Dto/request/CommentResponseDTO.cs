using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Entity.Dto.request
{
    public record CommentResponseDTO(long Id, string Comment, string Name, string Surname, string Email, long ProjectId, string Status);
}
