using ConnectUs.Entity.Model;
using ConnectUs.Service.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Service.Services.Concrete
{
    public class EmailService : IEmailService
    {
        private readonly string _fromEmail;
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _fromEmail = configuration["EmailSettings:FromEmail"];
            _smtpHost = configuration["EmailSettings:SmtpHost"];
            _smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
            _smtpUsername = configuration["EmailSettings:Username"];
            _smtpPassword = configuration["EmailSettings:Password"];
            _logger = logger;
        }

        public async Task SendMailAsync(MailModel mailModel)
        {
            try
            {
                using var smtpClient = new SmtpClient(_smtpHost, _smtpPort)
                {
                    Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_fromEmail),
                    Subject = "Aktivasyon İşlemleri",
                    IsBodyHtml = true,
                    Body = GenerateHtmlContent(mailModel)
                };

                mailMessage.To.Add(mailModel.Email);

                await smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation("Email sent successfully to {Email}", mailModel.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while sending email to {Email}", mailModel.Email);
                throw; // Hata yönetimini burada özelleştirebilirsiniz.
            }
        }

        private string GenerateHtmlContent(MailModel mailModel)
        {
            return $@"
            <html>
            <head>
                <style>
                    body {{ font-family: Arial, sans-serif; color: #333333; background-color: #f9f9f9; padding: 20px; }}
                    h3 {{ color: #2C3E50; }}
                    .content-container {{ background-color: #ffffff; padding: 20px; border-radius: 8px; box-shadow: 0 4px 6px rgba(0,0,0,0.1); }}
                    .code-box {{ background-color: #f0f8ff; padding: 15px; font-size: 18px; font-weight: bold; color: #34495e; border-radius: 5px; margin-top: 20px; }}
                    .footer {{ color: #7f8c8d; font-size: 12px; margin-top: 30px; }}
                    .footer em {{ font-style: italic; }}
                    hr {{ border: 1px solid #ddd; margin: 20px 0; }}
                </style>
            </head>
            <body>
                <div class='content-container'>
                    <p>Merhaba,</p>
                    <p>Hesabınızı etkinleştirmek için aşağıdaki aktivasyon kodunu kullanabilirsiniz:</p>
                    <div class='code-box'>
                        <strong>{mailModel.Code}</strong>
                    </div>
                    <hr>
                    <p><strong>Gizlilik Bildirgesi:</strong></p>
                    <p>Bu e-posta, yalnızca belirlenen alıcıya yönelik olarak gönderilmiştir ve içerdiği bilgiler gizli olabilir. Eğer yanlışlıkla bu e-postayı aldıysanız, lütfen gönderene bildiriniz ve mesajı siliniz. E-postadaki bilgilerin yetkisiz kişilerle paylaşılması yasaktır ve gizlilik politikasına aykırıdır.</p>
                    <p><em>Şirket Adı, tüm kullanıcı verilerini gizli tutmayı taahhüt eder. E-posta ile gönderilen bilgiler yalnızca belirtilen amaç için kullanılacaktır ve üçüncü şahıslarla paylaşılmayacaktır.</em></p>
                </div>
                <div class='footer'>
                    <p>Teşekkür ederiz</p>
                </div>
            </body>
            </html>";
        }
    }
}
