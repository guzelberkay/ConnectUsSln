using System;

using Microsoft.AspNetCore.Http;
using ConnectUs.Entity.Dto.request;
using ConnectUs.Entity.Dto.response;
using ConnectUs.Entity.Entities;
using ConnectUs.Core.Exceptions;
using ConnectUs.Data.Repositories.Abstractions;
using ConnectUs.Core.Utilities;
using ConnectUs.Service.Services.Abstractions;
using ConnectUs.Data.Repositories.Concretes;

namespace ConnectUs.Service.Services.Concrete
{
    public class OurServicesService : IOurServicesService
    {
        private readonly IOurServicesRepository _ourServicesRepository;
        private readonly IAuthRepository _authRepository;
        private readonly JwtTokenManager _jwtTokenManager;

        public OurServicesService(
            IOurServicesRepository ourServicesRepository,
            IAuthRepository authRepository,
            JwtTokenManager jwtTokenManager)
        {
            _ourServicesRepository = ourServicesRepository;
            _authRepository = authRepository;
            _jwtTokenManager = jwtTokenManager;
        }

        public async Task<bool> SaveAsync(OurServicesSaveRequestDTO dto)
        {
            var authId = ExtractAuthIdFromToken(dto.Token);
            var auth = await _authRepository.FindByIdAsync(authId) ?? throw new GeneralException(ErrorType.AUTH_NOT_FOUND);

            var ourServices = new OurServices
            {
                Title = dto.Title,
                Description = dto.Description,
                Name = dto.Photo.FileName,
                Type = dto.Photo.ContentType,
                File = ConvertFileToByteArray(dto.Photo)
            };

            await _ourServicesRepository.SaveAsync(ourServices);
            return true;
        }

        public async Task<bool> DeleteAsync(OurServicesDeleteRequestDTO dto)
        {
            var authId = ExtractAuthIdFromToken(dto.Token);
            var auth = await _authRepository.FindByIdAsync(authId) ?? throw new GeneralException(ErrorType.AUTH_NOT_FOUND);

            var ourServices = await _ourServicesRepository.FindByIdAsync(dto.OurServicesId) ?? throw new GeneralException(ErrorType.OURSERVICES_NOT_FOUND);
            await _ourServicesRepository.DeleteAsync(ourServices);
            return true;
        }

        public async Task<byte[]> GetOneImageAsync(long id)
        {
            var ourServices = await _ourServicesRepository.FindByIdAsync(id) ?? throw new Exception($"Image with {id} doesn't exist");
            return ourServices.File;
        }

        public async Task<List<OurServicesResponseDTO>> FindAllAsync()
        {
            var services = await _ourServicesRepository.FindAllAsync();
            return services.Select(service => new OurServicesResponseDTO
            {
                Id = service.Id,
                Title = service.Title,
                Description = service.Description,
                PhotoUrl = $"https://api.isttekzemin.com/api/ourservices/image/{service.Id}"
            }).ToList();
        }
    
        private long ExtractAuthIdFromToken(string token)
        {
            var authIdOptional = _jwtTokenManager.GetAuthIdFromToken(token);
            if (authIdOptional.HasValue)
                return authIdOptional.Value;

            throw new Exception("AuthId could not be extracted from token");
        }

        private byte[] ConvertFileToByteArray(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
