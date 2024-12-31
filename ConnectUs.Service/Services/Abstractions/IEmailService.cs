using ConnectUs.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Service.Services.Abstractions
{
    public interface IEmailService
    {
        // Mail gönderme işlevi
        Task SendMailAsync(MailModel mailModel);
    }
}
