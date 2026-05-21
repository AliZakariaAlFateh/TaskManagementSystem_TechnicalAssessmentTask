using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.Task;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.Shared.Responses;

namespace TaskManagementSystem.Application.Commands.Tasks
{
    public class UpdateTaskStatusCommandHandler : IRequestHandler<UpdateTaskStatusCommand, ApiResponse<TaskResponseDto>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;

        public UpdateTaskStatusCommandHandler(ITaskRepository taskRepository, IProjectRepository projectRepository)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
        }

        public async Task<ApiResponse<TaskResponseDto>> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.Id);
            if (task == null)
                return ApiResponse<TaskResponseDto>.FailResponse("Task not found");

            task.Status = request.Status;
            await _taskRepository.UpdateAsync(task);

            var project = await _projectRepository.GetByIdAsync(task.ProjectId);

            var result = new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                DueDate = task.DueDate,
                Priority = task.Priority.ToString(),
                ProjectId = task.ProjectId,
                ProjectName = project?.Name ?? string.Empty
            };

            return ApiResponse<TaskResponseDto>.SuccessResponse(result, "Task status updated successfully");
        }
    }
}
