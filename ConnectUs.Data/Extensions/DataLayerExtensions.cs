using ConnectUs.Data.Repositories.Abstractions;
using ConnectUs.Data.Repositories.Concretes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConnectUs.Data.Extensions
{
    public static class DataLayerExtensions
    {
        public static IServiceCollection LoadDataLayerExtensions(this IServiceCollection services, IConfiguration config)
        {
            // Jenerik olmayan bağımlılıkları kaydetme
          //  services.AddScoped<IAuthRepository, AuthRepository>();

            return services;
        }
    }
}
