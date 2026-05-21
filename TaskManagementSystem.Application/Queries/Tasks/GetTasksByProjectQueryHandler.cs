using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.Task;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.Shared.Responses;

namespace TaskManagementSystem.Application.Queries.Tasks
{
    public class GetTasksByProjectQueryHandler : IRequestHandler<GetTasksByProjectQuery, ApiResponse<IEnumerable<TaskResponseDto>>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;

        public GetTasksByProjectQueryHandler(ITaskRepository taskRepository, IProjectRepository projectRepository)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
        }

        public async Task<ApiResponse<IEnumerable<TaskResponseDto>>> Handle(GetTasksByProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (project == null)
                return ApiResponse<IEnumerable<TaskResponseDto>>.FailResponse("Project not found");

            var tasks = await _taskRepository.GetTasksByProjectAsync(request.ProjectId);

            var result = tasks.Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status.ToString(),
                DueDate = t.DueDate,
                Priority = t.Priority.ToString(),
                ProjectId = t.ProjectId,
                ProjectName = project.Name
            });

            return ApiResponse<IEnumerable<TaskResponseDto>>.SuccessResponse(result, "Tasks retrieved successfully");
        }
    }
}
