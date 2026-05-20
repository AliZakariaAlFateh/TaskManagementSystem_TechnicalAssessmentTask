using TaskManagementSystem.Application.DTOs.Project;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.Shared.Responses;

namespace TaskManagementSystem.API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ApiResponse<ProjectResponseDto>> CreateProjectAsync(ProjectDto dto)
        {
            var project = new Project
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _projectRepository.AddAsync(project);

            var result = new ProjectResponseDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedAt = project.CreatedAt,
                TasksCount = 0
            };

            return ApiResponse<ProjectResponseDto>.SuccessResponse(result, "Project created successfully");
        }

        public async Task<ApiResponse<IEnumerable<ProjectResponseDto>>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();

            var result = projects.Select(p => new ProjectResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreatedAt = p.CreatedAt,
                TasksCount = p.Tasks?.Count ?? 0
            });

            return ApiResponse<IEnumerable<ProjectResponseDto>>.SuccessResponse(result, "Projects retrieved successfully");
        }

        public async Task<ApiResponse<ProjectResponseDto>> GetProjectByIdAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
                return ApiResponse<ProjectResponseDto>.FailResponse("Project not found");

            var result = new ProjectResponseDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedAt = project.CreatedAt,
                TasksCount = project.Tasks?.Count ?? 0
            };

            return ApiResponse<ProjectResponseDto>.SuccessResponse(result, "Project retrieved successfully");
        }

        public async Task<ApiResponse<ProjectResponseDto>> UpdateProjectAsync(int id, ProjectDto dto)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
                return ApiResponse<ProjectResponseDto>.FailResponse("Project not found");

            project.Name = dto.Name;
            project.Description = dto.Description;

            await _projectRepository.UpdateAsync(project);

            var result = new ProjectResponseDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedAt = project.CreatedAt,
                TasksCount = project.Tasks?.Count ?? 0
            };

            return ApiResponse<ProjectResponseDto>.SuccessResponse(result, "Project updated successfully");
        }

        public async Task<ApiResponse<object>> DeleteProjectAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
                return ApiResponse<object>.FailResponse("Project not found");

            await _projectRepository.DeleteAsync(project);

            return ApiResponse<object>.SuccessResponse(project, "Project deleted successfully");
        }
    }
}
