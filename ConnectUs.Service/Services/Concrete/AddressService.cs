using ConnectUs.Core.Exceptions;
using ConnectUs.Core.Utilities;
using ConnectUs.Data.Repositories.Abstractions;
using ConnectUs.Data.Repositories.Concretes;
using ConnectUs.Entity.Dto.request;
using ConnectUs.Entity.Entities;
using ConnectUs.Service.Services.Abstractions;


namespace ConnectUs.Service.Services.Concrete
{
    public class AddressService : IAddressService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly JwtTokenManager _jwtTokenManager;

        public AddressService(IAuthRepository authRepository, IAddressRepository addressRepository, JwtTokenManager jwtTokenManager)
        {
            _authRepository = authRepository;
            _addressRepository = addressRepository;
            _jwtTokenManager = jwtTokenManager;
        }

        public async Task<bool> SaveAsync(AddressRequestDTO dto)
        {
            var authId = ExtractAuthIdFromToken(dto.Token);
            var auth = await _authRepository.FindByIdAsync(authId) ?? throw new GeneralException(ErrorType.AUTH_NOT_FOUND);

            var address = new Address
            {
                Description = dto.Description,
                Value = dto.Value
            };

            await _addressRepository.SaveAsync(address);
            return true;
        }

        public async Task<bool> DeleteAsync(string token, long id)
        {
            var authId = ExtractAuthIdFromToken(token);
            var auth = await _authRepository.FindByIdAsync(authId) ?? throw new GeneralException(ErrorType.AUTH_NOT_FOUND);

            var address = await _addressRepository.FindByIdAsync(id) ?? throw new GeneralException(ErrorType.CONTACT_NOT_FOUND);

            try
            {
                await _addressRepository.DeleteAsync(address);
                return true;
            }
            catch (Exception ex)
            {
                throw new GeneralException(ErrorType.INTERNAL_SERVER_ERROR);
            }
        }
        public List<Address> FindAll()
        {
            return _addressRepository.FindAll();
        }

        private long ExtractAuthIdFromToken(string token)
        {
            var authId = _jwtTokenManager.GetAuthIdFromToken(token);
            if (authId.HasValue)
            {
                return authId.Value;
            }
            throw new Exception("AuthId could not be extracted from token");
        }
    }
}