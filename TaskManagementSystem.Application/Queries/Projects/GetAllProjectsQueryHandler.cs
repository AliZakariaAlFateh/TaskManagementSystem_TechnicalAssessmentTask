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
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, ApiResponse<IEnumerable<ProjectResponseDto>>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ApiResponse<IEnumerable<ProjectResponseDto>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
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
    }
}
