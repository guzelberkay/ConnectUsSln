using ConnectUs.Entity.Dto.request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Service.Services.Abstractions
{
    public interface ICommentService
    {
        /// <summary>
        /// Saves a new comment.
        /// </summary>
        /// <param name="dto">The comment save request DTO.</param>
        /// <returns>A boolean indicating if the save operation was successful.</returns>
        Task<bool> SaveAsync(CommentSaveRequestDTO dto);

        /// <summary>
        /// Approves a comment.
        /// </summary>
        /// <param name="commentId">The ID of the comment to approve.</param>
        /// <param name="token">The JWT token of the approver.</param>
        /// <returns>A boolean indicating if the approval was successful.</returns>
        Task<bool> ApproveCommentAsync(long commentId, string token);

        /// <summary>
        /// Deletes a comment.
        /// </summary>
        /// <param name="dto">The comment delete request DTO.</param>
        /// <returns>A boolean indicating if the deletion was successful.</returns>
        Task<bool> DeleteAsync(CommentDeleteRequestDTO dto);

        /// <summary>
        /// Retrieves all comments.
        /// </summary>
        /// <returns>A list of comment response DTOs.</returns>
        Task<List<CommentResponseDTO>> GetAllCommentsAsync();

        /// <summary>
        /// Retrieves comments by project ID.
        /// </summary>
        /// <param name="projectId">The ID of the project for which to retrieve comments.</param>
        /// <returns>A list of comment response DTOs.</returns>
        Task<List<CommentResponseDTO>> GetCommentsByProjectIdAsync(long projectId);
    }

}
