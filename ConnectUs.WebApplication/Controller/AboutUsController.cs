using ConnectUs.Core.Exceptions;
using ConnectUs.Entity.Dto.request;
using ConnectUs.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ConnectUs.WebApplication.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AboutUsController : ControllerBase
    {
        private readonly IAboutUsService _aboutUsService;

        public AboutUsController(IAboutUsService aboutUsService)
        {
            _aboutUsService = aboutUsService;
        }

        /// <summary>
        /// Save or update the "About Us" information.
        /// </summary>
        /// <param name="dto">DTO containing "About Us" information.</param>
        /// <returns>Whether the operation was successful or not.</returns>
        [HttpPost("save-or-update")]
        public IActionResult SaveOrUpdate([FromBody] AboutUsRequestDTO dto)
        {
            try
            {
                var result = _aboutUsService.SaveOrUpdate(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Get the "About Us" information.
        /// </summary>
        /// <returns>The "About Us" information.</returns>
        [HttpGet("find")]
        public IActionResult Find()
        {
            try
            {
                var aboutUs = _aboutUsService.Find();
                return Ok(aboutUs);
            }
            catch (GeneralException ex) when (ex.ErrorType == ErrorType.BAD_REQUEST_ERROR)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
}


