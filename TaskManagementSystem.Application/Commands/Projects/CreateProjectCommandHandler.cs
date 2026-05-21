using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.Project;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.Shared.Responses;

namespace TaskManagementSystem.Application.Commands.Projects
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ApiResponse<ProjectResponseDto>>
    {
        private readonly IProjectRepository _projectRepository;

        public CreateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ApiResponse<ProjectResponseDto>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project
            {
                Name = request.Name,
                Description = request.Description
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
    }
}
