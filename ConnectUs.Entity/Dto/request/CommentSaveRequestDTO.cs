using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Entity.Dto.request
{
    public record CommentSaveRequestDTO(long ProjectId, string CompanyName, string Name, string Surname, string Email, string Comment);
}

