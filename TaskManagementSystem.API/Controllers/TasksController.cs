using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Application.DTOs.Task;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Persistence;
using TaskManagementSystem.Shared.Responses;

namespace TaskManagementSystem.API.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(TaskDto dto)
    {
        var result = await _taskService.CreateTaskAsync(dto);
        return Ok(result);
    }

    [HttpPut("updateStatus/{id}")]
    public async Task<IActionResult> UpdateStatus(int id, UpdateTaskStatusDto dto)
    {
        var result = await _taskService.UpdateTaskStatusAsync(id, dto);
        return Ok(result);
    }

    [HttpGet("getTsaksByProject/{projectId}")]
    public async Task<IActionResult> GetByProject(int projectId)
    {
        var response = await _taskService.GetTasksByProjectAsync(projectId);
        if (!response.Success)
            return NotFound(response);
        return Ok(response);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _taskService.DeleteTaskAsync(id);
        return Ok(response);
    }
}