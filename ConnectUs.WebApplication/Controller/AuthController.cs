using ConnectUs.Core.Exceptions;
using ConnectUs.Entity.Dto.request;
using ConnectUs.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ConnectUs.WebApplication.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Kullanıcı giriş işlemi
        /// </summary>
        /// <param name="dto">Giriş bilgileri (e-posta ve şifre)</param>
        /// <returns>Başarılı giriş durumunda JWT token</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO dto)
        {
            try
            {
                var token = await _authService.Login(dto);
                return Ok(new { Token = token });
            }
            catch (GeneralException ex)
            {
                return BadRequest(new { Error = ex.Message, ErrorCode = ex.ErrorType });
            }
        }

        /// <summary>
        /// Şifre sıfırlama işlemi
        /// </summary>
        /// <param name="dto">Şifre sıfırlama kodu ve yeni şifre bilgileri</param>
        /// <returns>Başarılı işlem durumunda true</returns>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDTO dto)
        {
            try
            {
                var result = await _authService.ResetPassword(dto);
                return Ok(new { Success = result });
            }
            catch (GeneralException ex)
            {
                return BadRequest(new { Error = ex.Message, ErrorCode = ex.ErrorType });
            }
        }

        /// <summary>
        /// Token üzerinden kullanıcı bilgisi alma
        /// </summary>
        /// <param name="token">JWT Token</param>
        /// <returns>Kullanıcı bilgileri</returns>
        [HttpGet("get-auth-from-token")]
        public async Task<IActionResult> GetAuthFromToken([FromQuery] string token)
        {
            try
            {
                var auth = await _authService.GetAuthFromToken(token);
                return Ok(auth);
            }
            catch (GeneralException ex)
            {
                return NotFound(new { Error = ex.Message, ErrorCode = ex.ErrorType });
            }
        }
    }
}
