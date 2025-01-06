using ConnectUs.Core.Exceptions;
using ConnectUs.Core.Utilities;
using ConnectUs.Data.Repositories.Abstractions;
using ConnectUs.Data.Repositories.Concretes;
using ConnectUs.Entity.Dto.request;
using ConnectUs.Entity.Entities;
using ConnectUs.Service.Services.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectUs.Service.Services.Concrete
{
    public class PhoneService : IPhoneService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IPhoneRepository _phoneRepository;
        private readonly JwtTokenManager _jwtTokenManager;
        private readonly ILogger<PhoneService> _logger;

        public PhoneService(IAuthRepository authRepository, IPhoneRepository phoneRepository, JwtTokenManager jwtTokenManager, ILogger<PhoneService> logger)
        {
            _authRepository = authRepository;
            _phoneRepository = phoneRepository;
            _jwtTokenManager = jwtTokenManager;
            _logger = logger;
        }

        public async Task<bool> SaveAsync(PhoneRequestDTO dto)
        {
            var authId = ExtractAuthIdFromToken(dto.Token);
            var auth = await _authRepository.FindByIdAsync(authId) ?? throw new GeneralException(ErrorType.AUTH_NOT_FOUND);

            var phone = new Phone
            {
                Description = dto.Description,
                Value = dto.Value
            };

            try
            {
                await _phoneRepository.SaveAsync(phone);
                _logger.LogInformation("Phone record saved successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving phone record.");
                throw new GeneralException(ErrorType.INTERNAL_SERVER_ERROR); // Ya da uygun hata
            }
        }

     
        public async Task<bool> DeleteAsync(string token, long id)
        {
            var authId = ExtractAuthIdFromToken(token);
            var auth = await _authRepository.FindByIdAsync(authId) ?? throw new GeneralException(ErrorType.AUTH_NOT_FOUND);

            var phone = await _phoneRepository.FindByIdAsync(id) ?? throw new GeneralException(ErrorType.CONTACT_NOT_FOUND);

            try
            {
                await _phoneRepository.DeleteAsync(phone);
                _logger.LogInformation("Phone record deleted successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting phone record.");
                throw new GeneralException(ErrorType.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> UpdateAsync(PhoneUpdateRequestDTO dto)
        {
            var authId = ExtractAuthIdFromToken(dto.Token);
            var auth = await _authRepository.FindByIdAsync(authId) ?? throw new GeneralException(ErrorType.AUTH_NOT_FOUND);

            var phone = await _phoneRepository.FindByIdAsync(dto.PhoneId) ?? throw new GeneralException(ErrorType.CONTACT_NOT_FOUND);

            phone.Description = dto.Description;
            phone.Value = dto.Value;

            try
            {
                await _phoneRepository.UpdateAsync(phone);
                _logger.LogInformation("Phone record updated successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating phone record.");
                throw new GeneralException(ErrorType.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<List<Phone>> FindAllAsync()
        {
            return await _phoneRepository.FindAllAsync();
        }

        private long ExtractAuthIdFromToken(string token)
        {
            var authId = _jwtTokenManager.GetAuthIdFromToken(token);
            if (authId.HasValue)
            {
                return authId.Value;
            }

            throw new InvalidOperationException("AuthId could not be extracted from token");
        }
    }
}
