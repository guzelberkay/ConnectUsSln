using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Core.Utilities
{
    public interface ITokenService
    {
        public Task<String> GenerateToken(long authId);
    }
}
