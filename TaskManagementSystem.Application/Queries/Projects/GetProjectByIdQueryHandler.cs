using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.Project;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.Shared.Responses;

namespace TaskManagementSystem.Application.Queries.Projects
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ApiResponse<ProjectResponseDto>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectByIdQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ApiResponse<ProjectResponseDto>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);
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
    }
}
