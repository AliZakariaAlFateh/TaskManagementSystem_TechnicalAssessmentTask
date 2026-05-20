using TaskManagementSystem.Application.DTOs.Task;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.Shared.Responses;

namespace TaskManagementSystem.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;

        public TaskService(ITaskRepository taskRepository, IProjectRepository projectRepository)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
        }

        public async Task<ApiResponse<TaskResponseDto>> CreateTaskAsync(TaskDto dto)
        {
            var project = await _projectRepository.GetByIdAsync(dto.ProjectId);
            if (project == null)
                return ApiResponse<TaskResponseDto>.FailResponse("Project not found");

            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                Priority = dto.Priority,
                Status = Status.Pending,
                ProjectId = dto.ProjectId
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

        public async Task<ApiResponse<TaskResponseDto>> UpdateTaskStatusAsync(int id, UpdateTaskStatusDto dto)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                return ApiResponse<TaskResponseDto>.FailResponse("Task not found");

            task.Status = dto.Status;
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

        public async Task<ApiResponse<IEnumerable<TaskResponseDto>>> GetTasksByProjectAsync(int projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null)
                return ApiResponse<IEnumerable<TaskResponseDto>>.FailResponse("Project not found");

            var tasks = await _taskRepository.GetTasksByProjectAsync(projectId);

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

        public async Task<ApiResponse<object>> DeleteTaskAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                return ApiResponse<object>.FailResponse("Task not found");

            await _taskRepository.DeleteAsync(task);

            return ApiResponse<object>.SuccessResponse(task, "Task deleted successfully");
        }

    }
}
