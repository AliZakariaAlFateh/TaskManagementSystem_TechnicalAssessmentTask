using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.Task;
using TaskManagementSystem.Shared.Responses;

namespace TaskManagementSystem.Application.Queries.Tasks
{
    public class GetTasksByProjectQuery : IRequest<ApiResponse<IEnumerable<TaskResponseDto>>>
    {
        public int ProjectId { get; set; }
    }
}
