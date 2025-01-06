using ConnectUs.Core.Exceptions;
using ConnectUs.Entity.Dto.request;
using ConnectUs.Entity.Entities;
using ConnectUs.Service.Services.Abstractions;
using ConnectUs.Service.Services.Concrete;
using Microsoft.AspNetCore.Mvc;
using static com.sun.net.httpserver.Authenticator;

namespace ConnectUs.WebApplication.Controller
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        /// <summary>
        /// Yeni bir adres kaydeder.
        /// </summary>
        /// <param name="dto">Adres bilgileri</param>
        /// <returns>İşlem sonucu</returns>
        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] AddressRequestDTO dto)
        {
            if (dto == null)
            {
                return BadRequest(new ResponseDTO<bool>
                {
                    Data = false,
                    Code = 400,
                    Message = "Invalid address data provided."
                });
            }

            try
            {
                var result = await _addressService.SaveAsync(dto);
                return Ok(new ResponseDTO<bool>
                {
                    Data = result,
                    Code = 200,
                    Message = "Address successfully registered."
                });
            }
            catch (GeneralException ex)
            {
                return BadRequest(new ResponseDTO<bool>
                {
                    Data = false,
                    Code = 400,
                    Message = $"Error saving address: {ex.Message}"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDTO<bool>
                {
                    Data = false,
                    Code = 500,
                    Message = $"An unexpected error occurred: {ex.Message}"
                });
            }
        }





        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] string token, [FromQuery] long id)
        {
            try
            {
                var success = await _addressService.DeleteAsync(token, id);
                return Ok(new ResponseDTO<bool>
                {
                    Data = success,
                    Code = 200,
                    Message = success ? "Address successfully deleted" : "Address not found"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDTO<bool>
                {
                    Data = false,
                    Code = 400,
                    Message = $"Error deleting phone: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Tüm adresleri listeler.
        /// </summary>
        /// <returns>Adres listesi</returns>
        [HttpGet("all")]
        public ActionResult<List<Address>> GetAll()
        {
            try
            {
                var addresses = _addressService.FindAll();
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
