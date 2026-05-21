using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.Task;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.Shared.Responses;

namespace TaskManagementSystem.Application.Commands.Tasks
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, ApiResponse<TaskResponseDto>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;

        public CreateTaskCommandHandler(ITaskRepository taskRepository, IProjectRepository projectRepository)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
        }

        public async Task<ApiResponse<TaskResponseDto>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (project == null)
                return ApiResponse<TaskResponseDto>.FailResponse("Project not found");

            var task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                Priority = request.Priority,
                Status = Status.Pending,
                ProjectId = request.ProjectId
            };

            await _taskRepository.AddAsync(task);

            var result = new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                DueDate = task.DueDate,
                Priority = task.Priority.ToString(),
                ProjectId = task.ProjectId,
                ProjectName = project.Name
            };

            return ApiResponse<TaskResponseDto>.SuccessResponse(result, "Task created successfully");
        }
    }
}
