using ConnectUs.Entity.Dto.request;
using ConnectUs.Entity.Entities;
using ConnectUs.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult Save([FromBody] AddressRequestDTO dto)
        {
            try
            {
                var result = _addressService.Save(dto);
                if (result)
                {
                    return Ok(new { message = "Address saved successfully" });
                }
                return BadRequest(new { message = "Failed to save address" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Bir adresi siler.
        /// </summary>
        /// <param name="token">Kullanıcı token'ı</param>
        /// <param name="id">Silinecek adres ID'si</param>
        /// <returns>İşlem sonucu</returns>
        [HttpDelete("delete/{id}")]
        public ActionResult Delete(string token, long id)
        {
            try
            {
                var result = _addressService.Delete(token, id);
                if (result)
                {
                    return Ok(new { message = "Address deleted successfully" });
                }
                return BadRequest(new { message = "Failed to delete address" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Adresi günceller.
        /// </summary>
        /// <param name="dto">Güncelleme bilgileri</param>
        /// <returns>İşlem sonucu</returns>
        [HttpPut("update")]
        public ActionResult Update([FromBody] AddressUpdateRequestDTO dto)
        {
            try
            {
                var result = _addressService.Update(dto);
                if (result)
                {
                    return Ok(new { message = "Address updated successfully" });
                }
                return BadRequest(new { message = "Failed to update address" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
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
