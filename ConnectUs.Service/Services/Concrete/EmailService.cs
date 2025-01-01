using ConnectUs.Entity.Model;
using ConnectUs.Service.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using System;

public class EmailService : IEmailService
{
    private readonly string _fromEmail = "java.teamworks@gmail.com";
    private readonly string _smtpHost = "smtp.gmail.com";
    private readonly int _smtpPort = 587;
    private readonly string _smtpUsername = "java.teamworks@gmail.com";
    private readonly string _smtpPassword = "qzlq qqhh fhdz ylhk"; // Bu değeri çevresel değişken veya başka bir güvenli yöntemle alabilirsiniz.
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }

    // Asenkron Mail Gönderim
    public async Task SendMailAsync(MailModel mailModel)
    {
        try
        {
            // E-posta oluşturuluyor
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_fromEmail));
            email.To.Add(MailboxAddress.Parse(mailModel.Email)); // mailModel.Email burada alıcı e-posta adresi
            email.Subject = "Aktivasyon Kodu";  // Dinamik konu, ihtiyaca göre değiştirilebilir.
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = GenerateHtmlContent(mailModel)
            };

            // SMTP ile asenkron e-posta gönderme
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_smtpHost, _smtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_smtpUsername, _smtpPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            _logger.LogInformation("Email successfully sent to {Recipient}", mailModel.Email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while sending the email.");
            throw;
        }
    }

    private string GenerateHtmlContent(MailModel mailModel)
    {
        // HTML template for email
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
