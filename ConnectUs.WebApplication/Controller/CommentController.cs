using ConnectUs.Core.Exceptions;
using ConnectUs.Entity.Dto.request;
using ConnectUs.Service.Services.Abstractions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ConnectUs.WebApplication.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveComment([FromBody] CommentSaveRequestDTO dto)
        {
            var isSaved = await _commentService.SaveAsync(dto);
            var response = new ResponseDTO<bool>
            {
                Data = isSaved,
                Code = 200,
                Message = "The comment was saved peacefully."
            };
            return Ok(response);
        }

        [HttpPut("approve")]
        public async Task<IActionResult> ApproveComment([FromBody] ApproveCommentRequestDTO dto)
        {
            var isApproved = await _commentService.ApproveCommentAsync(dto.Id, dto.Token);
            var response = new ResponseDTO<bool>
            {
                Data = isApproved,
                Code = 200,
                Message = "The comment was approved successfully."
            };
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteComment([FromBody] CommentDeleteRequestDTO dto)
        {
            var isDeleted = await _commentService.DeleteAsync(dto);
            var response = new ResponseDTO<bool>
            {
                Data = isDeleted,
                Code = 200,
                Message = "The comment was deleted peacefully."
            };
            return Ok(response);
        }

        [HttpGet("findall")]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentService.GetAllCommentsAsync();
            return Ok(comments);
        }

        [HttpGet("findallbyprojectid")]
        public async Task<IActionResult> GetCommentsByProjectId([FromQuery] long projectId)
        {
            var comments = await _commentService.GetCommentsByProjectIdAsync(projectId);
            return Ok(comments);
        }
    }
}

