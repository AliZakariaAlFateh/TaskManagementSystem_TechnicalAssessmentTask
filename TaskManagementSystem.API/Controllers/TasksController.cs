using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Application.Commands.Tasks;
using TaskManagementSystem.Application.DTOs.Task;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Application.Queries.Tasks;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Persistence;
using TaskManagementSystem.Shared.Responses;

namespace TaskManagementSystem.API.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]

//Without CQRS
//public class TasksController : ControllerBase
//{
//    private readonly ITaskService _taskService;

//    public TasksController(ITaskService taskService)
//    {
//        _taskService = taskService;
//    }

//    [HttpPost("create")]
//    public async Task<IActionResult> Create(TaskDto dto)
//    {
//        var result = await _taskService.CreateTaskAsync(dto);
//        return Ok(result);
//    }

//    [HttpPut("updateStatus/{id}")]
//    public async Task<IActionResult> UpdateStatus(int id, UpdateTaskStatusDto dto)
//    {
//        var result = await _taskService.UpdateTaskStatusAsync(id, dto);
//        return Ok(result);
//    }

//    [HttpGet("getTsaksByProject/{projectId}")]
//    public async Task<IActionResult> GetByProject(int projectId)
//    {
//        var response = await _taskService.GetTasksByProjectAsync(projectId);
//        if (!response.Success)
//            return NotFound(response);
//        return Ok(response);
//    }

//    [HttpDelete("delete/{id}")]
//    public async Task<IActionResult> Delete(int id)
//    {
//        var response = await _taskService.DeleteTaskAsync(id);
//        return Ok(response);
//    }
//}




//With CQRS and MediatR
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateTaskCommand command)
    {
        var response = await _mediator.Send(command);
        if (!response.Success)
            return BadRequest(response);
        return Ok(response);
    }

    [HttpPut("updateStatus")]
    public async Task<IActionResult> UpdateStatus(UpdateTaskStatusCommand command)
    {
        //command.Id = id;
        var response = await _mediator.Send(command);
        if (!response.Success)
            return NotFound(response);
        return Ok(response);
    }

    [HttpGet("getByProject/{projectId}")]
    public async Task<IActionResult> GetByProject(int projectId)
    {
        var response = await _mediator.Send(new GetTasksByProjectQuery { ProjectId = projectId });
        if (!response.Success)
            return NotFound(response);
        return Ok(response);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _mediator.Send(new DeleteTaskCommand { Id = id });
        if (!response.Success)
            return NotFound(response);
        return Ok(response);
    }
}