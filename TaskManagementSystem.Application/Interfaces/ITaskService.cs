using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.Task;
using TaskManagementSystem.Shared.Responses;

namespace TaskManagementSystem.Application.Interfaces
{
    public interface ITaskService
    {
        Task<ApiResponse<TaskResponseDto>> CreateTaskAsync(TaskDto dto);
        Task<ApiResponse<TaskResponseDto>> UpdateTaskStatusAsync(int id, UpdateTaskStatusDto dto);
        Task<ApiResponse<IEnumerable<TaskResponseDto>>> GetTasksByProjectAsync(int projectId);
        Task<ApiResponse<object>> DeleteTaskAsync(int id);
    }
}
