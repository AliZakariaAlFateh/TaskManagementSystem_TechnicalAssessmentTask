using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.Project;
using TaskManagementSystem.Shared.Responses;

namespace TaskManagementSystem.Application.Commands.Projects
{
    public class CreateProjectCommand : IRequest<ApiResponse<ProjectResponseDto>>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
