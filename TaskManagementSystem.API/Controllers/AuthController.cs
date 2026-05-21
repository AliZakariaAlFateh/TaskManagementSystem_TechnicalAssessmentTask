using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Commands.Auth;
using TaskManagementSystem.Application.DTOs.Auth;
using TaskManagementSystem.Application.Interfaces;
namespace TaskManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]

//Without CQRS ..
//public class AuthController : ControllerBase
//{

//    private readonly IAuthService _authService;

//    public AuthController(IAuthService authService)
//    {
//        _authService = authService;
//    }

//    [HttpPost("register")]
//    public async Task<IActionResult> Register(RegisterDto dto)
//    {
//        var response = await _authService.RegisterAsync(dto);
//        if (!response.Success)
//        {
//            if (response.Message.Contains("already exists"))
//                return Conflict(response);
//            return BadRequest(response);
//        }
//        return Ok(response);
//    }

//    [HttpPost("login")]
//    public async Task<IActionResult> Login(LoginDto dto)
//    {
//        var response = await _authService.LoginAsync(dto);
//        if (!response.Success)
//            return Unauthorized(response);
//        return Ok(response);
//    }
//}


//With CQRS and MediatR
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        var response = await _mediator.Send(command);
        if (!response.Success)
        {
            if (response.Message.Contains("already exists"))
                return Conflict(response);
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var response = await _mediator.Send(command);
        if (!response.Success)
            return Unauthorized(response);
        return Ok(response);
    }
}