using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.Shared.Responses;

namespace TaskManagementSystem.Application.Commands.Projects
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, ApiResponse<object>>
    {
        private readonly IProjectRepository _projectRepository;  
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteProjectCommandHandler(
            IProjectRepository projectRepository,  
            IHttpContextAccessor httpContextAccessor)
        {
            _projectRepository = projectRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<object>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null)
                return ApiResponse<object>.FailResponse("User not authenticated");

            var isAdmin = user.IsInRole("Admin");
            if (!isAdmin)
            {
                return ApiResponse<object>.FailResponse(
                    "You don't have permission to delete projects. Only Admin users can delete projects.",
                    new List<string> { "Admin role is required for this action" }
                );
            }

            var project = await _projectRepository.GetByIdAsync(request.Id);
            if (project == null)
                return ApiResponse<object>.FailResponse("Project not found");

            await _projectRepository.DeleteAsync(project);

            return ApiResponse<object>.SuccessResponse(project, "Project deleted successfully");
        }
    }
}
