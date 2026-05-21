using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.Auth;
using TaskManagementSystem.Shared.Responses;

namespace TaskManagementSystem.Application.Commands.Auth
{
    public class LoginCommand : IRequest<ApiResponse<AuthResponseDto>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
