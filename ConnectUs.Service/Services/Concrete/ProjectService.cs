using ConnectUs.Core.Exceptions;
using ConnectUs.Core.Utilities;
using ConnectUs.Data.Repositories.Abstractions;
using ConnectUs.Entity.Dto.request;
using ConnectUs.Entity.Entities;
using ConnectUs.Service.Services.Abstractions;

namespace ConnectUs.Service.Services.Concrete
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IAuthRepository _authRepository;
        private readonly JwtTokenManager _jwtTokenManager;

        public ProjectService(
            IProjectRepository projectRepository,
            IAuthRepository authRepository,
            JwtTokenManager jwtTokenManager)
        {
            _projectRepository = projectRepository;
            _authRepository = authRepository;
            _jwtTokenManager = jwtTokenManager;
        }

        public async Task<bool> SaveAsync(ProjectSaveRequestDTO dto)
        {
            var authId = ExtractAuthIdFromToken(dto.Token);

            var auth = await _authRepository.FindByIdAsync(authId)
                        ?? throw new GeneralException(ErrorType.AUTH_NOT_FOUND);

            var project = new Project
            {
                Employer = dto.Employer,
                Title = dto.Title,
                Location = dto.Location,
                Date = dto.Date,
                Description = dto.Description
            };

            await _projectRepository.SaveAsync(project);
            return true;
        }

        public async Task<bool> DeleteAsync(ProjectDeleteRequestDTO dto)
        {
            var authId = ExtractAuthIdFromToken(dto.Token);

            var auth = await _authRepository.FindByIdAsync(authId)
                        ?? throw new GeneralException(ErrorType.AUTH_NOT_FOUND);

            var project = await _projectRepository.FindByIdAsync(dto.ProjectId)
                          ?? throw new GeneralException(ErrorType.PROJECT_NOT_FOUND);

            await _projectRepository.DeleteAsync(project);
            return true;
        }

        public async Task<List<Project>> FindAllAsync()
        {
            return await _projectRepository.FindAllAsync();
        }

        public async Task<Project> FindProjectByIdAsync(long projectId)
        {
            return await _projectRepository.FindByIdAsync(projectId)
                   ?? throw new GeneralException(ErrorType.PROJECT_NOT_FOUND);
        }

        private long ExtractAuthIdFromToken(string token)
        {
            return _jwtTokenManager.GetAuthIdFromToken(token)
                   ?? throw new GeneralException(ErrorType.TOKEN_INVALID);
        }
    }
}

