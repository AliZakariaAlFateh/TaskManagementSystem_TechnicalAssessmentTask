using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.DTOs.Project;
using TaskManagementSystem.Application.Interfaces;
namespace TaskManagementSystem.API.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(ProjectDto dto)
    {
        var response = await _projectService.CreateProjectAsync(dto);
        if (!response.Success)
            return BadRequest(response);
        return Ok(response);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _projectService.GetAllProjectsAsync();
        return Ok(response);
    }

    [HttpGet("getById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _projectService.GetProjectByIdAsync(id);
        if (!response.Success)
            return NotFound(response);
        return Ok(response);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, ProjectDto dto)
    {
        var response = await _projectService.UpdateProjectAsync(id, dto);
        if (!response.Success)
            return NotFound(response);
        return Ok(response);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _projectService.DeleteProjectAsync(id);
        if (!response.Success)
            return NotFound(response);
        return Ok(response);
    }
}