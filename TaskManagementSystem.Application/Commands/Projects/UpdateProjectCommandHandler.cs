using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.Project;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.Shared.Responses;

namespace TaskManagementSystem.Application.Commands.Projects
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ApiResponse<ProjectResponseDto>>
    {
        private readonly IProjectRepository _projectRepository;

        public UpdateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ApiResponse<ProjectResponseDto>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);
            if (project == null)
                return ApiResponse<ProjectResponseDto>.FailResponse("Project not found");

            project.Name = request.Name;
            project.Description = request.Description;

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
    }
}
