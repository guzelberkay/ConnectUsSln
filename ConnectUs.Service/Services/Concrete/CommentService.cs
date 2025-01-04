using ConnectUs.Core.Exceptions;
using ConnectUs.Core.Utilities;
using ConnectUs.Data.Repositories.Abstractions;
using ConnectUs.Entity.Dto.request;
using ConnectUs.Entity.Entities;
using ConnectUs.Entity.Entities.Enums;
using ConnectUs.Service.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Service.Services.Concrete
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IAuthRepository _authRepository;
        private readonly JwtTokenManager _jwtTokenManager;

        public CommentService(ICommentRepository commentRepository, IAuthRepository authRepository, JwtTokenManager jwtTokenManager)
        {
            _commentRepository = commentRepository;
            _authRepository = authRepository;
            _jwtTokenManager = jwtTokenManager;
        }

        public async Task<bool> SaveAsync(CommentSaveRequestDTO dto)
        {
            var comment = new Comment
            {
                ProjectId = dto.ProjectId,
                CompanyName = dto.CompanyName,
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                CommentContent = dto.Comment,
                Status = EStatus.PENDING
            };

            await _commentRepository.SaveAsync(comment);
            return true;
        }

        public async Task<bool> ApproveCommentAsync(long commentId, string token)
        {
            var authId = ExtractAuthIdFromToken(token);

            var auth = await _authRepository.FindByIdAsync(authId)
                        ?? throw new GeneralException(ErrorType.AUTH_NOT_FOUND);

            var comment = await _commentRepository.FindByIdAsync(commentId)
                         ?? throw new GeneralException(ErrorType.COMMENT_NOT_FOUND);

            if (comment.Status == EStatus.PENDING)
            {
                comment.Status = EStatus.ACTIVE;
                await _commentRepository.SaveAsync(comment);
                return true;
            }

            throw new GeneralException(ErrorType.COMMENT_ALREADY_APPROVED_OR_REJECTED);
        }

        public async Task<bool> DeleteAsync(CommentDeleteRequestDTO dto)
        {
            var authId = ExtractAuthIdFromToken(dto.Token);

            var auth = await _authRepository.FindByIdAsync(authId)
                        ?? throw new GeneralException(ErrorType.AUTH_NOT_FOUND);

            var comment = await _commentRepository.FindByIdAsync(dto.CommentId)
                         ?? throw new GeneralException(ErrorType.COMMENT_NOT_FOUND);

            await _commentRepository.DeleteAsync(comment);
            return true;
        }

        public async Task<List<CommentResponseDTO>> GetAllCommentsAsync()
        {
            var comments = await _commentRepository.GetAllAsync();
            return comments.Select(c => new CommentResponseDTO(
                c.Id,                    // Assuming `c` has an `Id` property
                c.CommentContent,               // Assuming `c` has a `Comment` property
                c.Name,                  // Assuming `c` has a `Name` property
                c.Surname,               // Assuming `c` has a `Surname` property
                c.Email,                 // Assuming `c` has an `Email` property
                c.ProjectId,             // Assuming `c` has a `ProjectId` property
                c.Status.ToString()      // Convert `EStatus` enum to string
            )).ToList();
        }



        public async Task<List<CommentResponseDTO>> GetCommentsByProjectIdAsync(long projectId)
        {
            var comments = await _commentRepository.FindByProjectIdAsync(projectId);
            return comments.Select(c => new CommentResponseDTO(
                c.Id,                    // Assuming `c` has an `Id` property
                c.CommentContent,               // Assuming `c` has a `Comment` property
                c.Name,                  // Assuming `c` has a `Name` property
                c.Surname,               // Assuming `c` has a `Surname` property
                c.Email,                 // Assuming `c` has an `Email` property
                c.ProjectId,             // Assuming `c` has a `ProjectId` property
                c.Status.ToString()      // Convert `EStatus` enum to string
            )).ToList();
        }


        private long ExtractAuthIdFromToken(string token)
        {
            var authId = _jwtTokenManager.GetAuthIdFromToken(token);
            if (authId.HasValue)
                return authId.Value;

            throw new Exception("AuthId could not be extracted from token");
        }
    }
}

