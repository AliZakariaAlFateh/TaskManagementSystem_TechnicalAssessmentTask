using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.Shared.Responses;

namespace TaskManagementSystem.Application.Commands.Tasks
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, ApiResponse<object>>
    {
        private readonly ITaskRepository _taskRepository;

        public DeleteTaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<ApiResponse<object>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.Id);
            if (task == null)
                return ApiResponse<object>.FailResponse("Task not found");

            await _taskRepository.DeleteAsync(task);

            return ApiResponse<object>.SuccessResponse(task, "Task deleted successfully");
        }
    }
}
