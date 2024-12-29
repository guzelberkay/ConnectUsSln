using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Core.Exceptions
{
    public class ErrorMessage
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<string>? Fields { get; set; } = new();
    }
}
