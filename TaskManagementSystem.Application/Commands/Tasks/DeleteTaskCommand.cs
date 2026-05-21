using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Shared.Responses;

namespace TaskManagementSystem.Application.Commands.Tasks
{
    public class DeleteTaskCommand : IRequest<ApiResponse<object>>
    {
        public int Id { get; set; }
    }
}
