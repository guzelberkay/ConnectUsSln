using ConnectUs.Core.Exceptions;
using ConnectUs.Core.Utilities;
using ConnectUs.Data.Repositories.Abstractions;
using ConnectUs.Entity.Dto.request;
using ConnectUs.Entity.Entities;
using ConnectUs.Service.Services.Abstractions;

namespace ConnectUs.Service.Services.Concrete
{
    public class AboutUsService : IAboutUsService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IAboutUsRepository _aboutUsRepository;
        private readonly JwtTokenManager _jwtTokenManager;

        public AboutUsService(IAuthRepository authRepository, IAboutUsRepository aboutUsRepository, JwtTokenManager jwtTokenManager)
        {
            _authRepository = authRepository;
            _aboutUsRepository = aboutUsRepository;
            _jwtTokenManager = jwtTokenManager;
        }

        public bool SaveOrUpdate(AboutUsRequestDTO dto)
        {
            var aboutUs = _aboutUsRepository.FindFirst();

            if (aboutUs == null)
            {
                // Eğer "Hakkımızda" bilgisi yoksa yeni oluştur
                aboutUs = new AboutUs
                {
                    Content = dto.Content
                };
            }
            else
            {
                // Mevcut bilgiyi güncelle
                if (!string.IsNullOrEmpty(dto.Content))
                {
                    aboutUs.Content = dto.Content;
                }
            }

            _aboutUsRepository.Save(aboutUs);
            return true;
        }

        public AboutUs Find()
        {
            var aboutUs = _aboutUsRepository.FindFirst();

            if (aboutUs == null)
                throw new GeneralException(ErrorType.AUTH_NOT_FOUND);

            return aboutUs;
        }
    }
}
