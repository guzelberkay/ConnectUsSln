using ConnectUs.Core.Exceptions;
using ConnectUs.Entity.Dto.request;
using ConnectUs.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ConnectUs.WebApplication.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class PhoneController : ControllerBase
    {
        private readonly IPhoneService _phoneService;

        public PhoneController(IPhoneService phoneService)
        {
            _phoneService = phoneService;
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] PhoneRequestDTO dto)
        {
            try
            {
                var success = await _phoneService.SaveAsync(dto);
                return Ok(new ResponseDTO<bool>
                {
                    Data = success,
                    Code = 200,
                    Message = "Phone successfully registered"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDTO<bool>
                {
                    Data = false,
                    Code = 400,
                    Message = $"Error saving phone: {ex.Message}"
                });
            }
        }

        [HttpGet("findall")]
        public async Task<IActionResult> FindAll()
        {
            try
            {
                var phones = await _phoneService.FindAllAsync();
                return Ok(phones);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving phones: {ex.Message}");
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] PhoneUpdateRequestDTO dto)
        {
            try
            {
                var success = await _phoneService.UpdateAsync(dto);
                return Ok(new ResponseDTO<bool>
                {
                    Data = success,
                    Code = 200,
                    Message = "Phone successfully updated"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDTO<bool>
                {
                    Data = false,
                    Code = 400,
                    Message = $"Error updating phone: {ex.Message}"
                });
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] string token, [FromQuery] long id)
        {
            try
            {
                var success = await _phoneService.DeleteAsync(token, id);
                return Ok(new ResponseDTO<bool>
                {
                    Data = success,
                    Code = 200,
                    Message = success ? "Phone successfully deleted" : "Phone not found"
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
    }
}
