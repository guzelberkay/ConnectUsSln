using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Entity.Dto.request
{
    public record PhoneUpdateRequestDTO(string Token, long PhoneId, string Description, string Value);
}
