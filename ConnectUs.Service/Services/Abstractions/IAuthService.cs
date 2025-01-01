using ConnectUs.Entity.Dto.request;
using ConnectUs.Entity.Entities;
using System.Threading.Tasks;

namespace ConnectUs.Service.Services.Abstractions
{
    public interface IAuthService
    {
        // Giriş yapma işlevi
        Task<string> Login(LoginRequestDTO dto);

        // Şifre sıfırlama işlevi
        Task<bool> ResetPassword(ResetPasswordRequestDTO dto);

        // Şifre unutma işlevi
        Task<string> ForgetPasswordAsync(string email);

        // Token'den Auth bilgisi alma işlevi
        Task<Auth> GetAuthFromToken(string token);

        // Token'den AuthId çıkarma
        Task<long> ExtractAuthIdFromToken(string token);
    }
}
