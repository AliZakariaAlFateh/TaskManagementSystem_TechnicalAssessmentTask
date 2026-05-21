using MediatR;
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

        public DeleteProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ApiResponse<object>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);
            if (project == null)
                return ApiResponse<object>.FailResponse("Project not found");

            await _projectRepository.DeleteAsync(project);

            return ApiResponse<object>.SuccessResponse(project, "Project deleted successfully");
        }
    }
}
