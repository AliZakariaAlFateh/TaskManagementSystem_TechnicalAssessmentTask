using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.Task;
using TaskManagementSystem.Domain.Enums;
using TaskManagementSystem.Shared.Responses;

namespace TaskManagementSystem.Application.Commands.Tasks
{
    public class CreateTaskCommand : IRequest<ApiResponse<TaskResponseDto>>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; } = DateTime.UtcNow.AddDays(7);
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public int ProjectId { get; set; }
    }
}
