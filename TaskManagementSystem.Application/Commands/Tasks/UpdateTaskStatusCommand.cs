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
    public class UpdateTaskStatusCommand : IRequest<ApiResponse<TaskResponseDto>>
    {
        public int Id { get; set; }
        public Status Status { get; set; }
    }
}
