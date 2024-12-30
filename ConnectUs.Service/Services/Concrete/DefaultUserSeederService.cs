using ConnectUs.Entity.Entities;
using ConnectUs.Data.Context;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConnectUs.Services
{
    public class DefaultUserSeederService : IHostedService
    {
        private readonly ILogger<DefaultUserSeederService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public DefaultUserSeederService(ILogger<DefaultUserSeederService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())  // Create a scope for scoped services
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Varsayılan kullanıcı bilgileri
                string email = "isttekzemin@gmail.com";
                string password = "Herekeliyim59.";

                // Şifreyi hashle
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                // Email ile kullanıcı var mı kontrol et
                var existingUser = await context.Auth
                    .FirstOrDefaultAsync(a => a.Email == email, cancellationToken);  // FirstOrDefaultAsync works here

                if (existingUser == null)
                {
                    var newUser = new Auth
                    {
                        Email = email,
                        Password = hashedPassword,
                 
                    };

                    await context.Auth.AddAsync(newUser, cancellationToken);
                    await context.SaveChangesAsync(cancellationToken);

                    _logger.LogInformation($"Varsayılan kullanıcı başarıyla oluşturuldu: {email}");
                }
                else
                {
                    _logger.LogInformation($"Kullanıcı zaten mevcut: {email}");
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Uygulama durdurulurken yapılacak işlemler (gerekirse)
            return Task.CompletedTask;
        }
    }
}
