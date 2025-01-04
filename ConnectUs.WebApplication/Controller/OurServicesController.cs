using ConnectUs.Core.Exceptions;
using ConnectUs.Core.Utilities;
using ConnectUs.Data.Repositories.Concretes;
using ConnectUs.Entity.Dto.request;
using ConnectUs.Entity.Dto.response;
using ConnectUs.Entity.Entities;
using ConnectUs.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ConnectUs.WebApplication.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class OurServicesController : ControllerBase
    {
        private readonly IOurServicesService _ourServicesService;

        public OurServicesController(IOurServicesService ourServicesService)
        {
            _ourServicesService = ourServicesService;
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveAsync([FromForm] OurServicesSaveRequestDTO dto)
        {
            try
            {
                var result = await _ourServicesService.SaveAsync(dto);
                return Ok(new { Success = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] OurServicesDeleteRequestDTO dto)
        {
            try
            {
                var result = await _ourServicesService.DeleteAsync(dto);
                return Ok(new { Success = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("image/{id}")]
        public async Task<IActionResult> GetOneImageAsync(long id)
        {
            try
            {
                var file = await _ourServicesService.GetOneImageAsync(id);
                return File(file, "application/octet-stream");
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }

        [HttpGet("findall")]
        public async Task<IActionResult> FindAllAsync()
        {
            try
            {
                var services = await _ourServicesService.FindAllAsync();
                return Ok(services);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
