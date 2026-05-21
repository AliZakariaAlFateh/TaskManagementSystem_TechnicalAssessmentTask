using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.Project;
using TaskManagementSystem.Shared.Responses;

namespace TaskManagementSystem.Application.Queries.Projects
{
    public class GetProjectByIdQuery : IRequest<ApiResponse<ProjectResponseDto>>
    {
        public int Id { get; set; }
    }
}
