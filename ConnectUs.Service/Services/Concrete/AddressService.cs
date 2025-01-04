using ConnectUs.Core.Exceptions;
using ConnectUs.Core.Utilities;
using ConnectUs.Data.Repositories.Abstractions;
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

        public bool Save(AddressRequestDTO dto)
        {
            var authId = ExtractAuthIdFromToken(dto.Token);
            var auth = _authRepository.FindByIdAsync(authId) ?? throw new GeneralException(ErrorType.AUTH_NOT_FOUND);

            var address = new Address
            {
                Description = dto.Description,
                Value = dto.Value
            };

            _addressRepository.Save(address);
            return true;
        }

        public bool Delete(string token, long id)
        {
            var authId = ExtractAuthIdFromToken(token);
            var auth = _authRepository.FindByIdAsync(authId) ?? throw new GeneralException(ErrorType.AUTH_NOT_FOUND);

            var address = _addressRepository.FindById(id) ?? throw new GeneralException(ErrorType.CONTACT_NOT_FOUND);

            _addressRepository.Delete(address);
            return true;
        }

        public bool Update(AddressUpdateRequestDTO dto)
        {
            var authId = ExtractAuthIdFromToken(dto.token);
            var auth = _authRepository.FindByIdAsync(authId) ?? throw new GeneralException(ErrorType.AUTH_NOT_FOUND);

            var address = _addressRepository.FindById(dto.adressId) ?? throw new GeneralException(ErrorType.CONTACT_NOT_FOUND);

            address.Description = dto.description;
            address.Value = dto.value;

            _addressRepository.Save(address);
            return true;
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