using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.Project;
using TaskManagementSystem.Shared.Responses;

namespace TaskManagementSystem.Application.Interfaces
{
    public interface IProjectService
    {
        Task<ApiResponse<ProjectResponseDto>> CreateProjectAsync(ProjectDto dto);
        Task<ApiResponse<IEnumerable<ProjectResponseDto>>> GetAllProjectsAsync();
        Task<ApiResponse<ProjectResponseDto>> GetProjectByIdAsync(int id);
        Task<ApiResponse<ProjectResponseDto>> UpdateProjectAsync(int id, ProjectDto dto);
        Task<ApiResponse<object>> DeleteProjectAsync(int id);
    }
}
