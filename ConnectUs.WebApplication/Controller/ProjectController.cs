using ConnectUs.Entity.Dto.request;
using ConnectUs.Service.Services.Abstractions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ConnectUs.WebApplication.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// Yeni bir proje kaydetme endpoint'i
        /// </summary>
        [HttpPost("save")]
        public async Task<IActionResult> SaveProject([FromBody] ProjectSaveRequestDTO dto)
        {
            var isSaved = await _projectService.SaveAsync(dto);
            return Ok(isSaved); // Başarı durumunu döner
        }

        /// <summary>
        /// Bir projeyi silme endpoint'i
        /// </summary>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteProject([FromBody] ProjectDeleteRequestDTO dto)
        {
            var isDeleted = await _projectService.DeleteAsync(dto);
            return Ok(isDeleted); // Silme işleminin durumunu döner
        }

        /// <summary>
        /// Tüm projeleri listeleme endpoint'i
        /// </summary>
        [HttpGet("findall")]
        public async Task<IActionResult> FindAllProjects()
        {
            var projects = await _projectService.FindAllAsync();
            return Ok(projects); // Tüm projeleri döner
        }
    }
}

