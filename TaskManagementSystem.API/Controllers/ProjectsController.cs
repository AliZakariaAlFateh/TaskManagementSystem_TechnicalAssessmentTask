using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Commands.Projects;
using TaskManagementSystem.Application.DTOs.Project;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Application.Queries.Projects;
namespace TaskManagementSystem.API.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]

//Without CQRS
//public class ProjectsController : ControllerBase
//{
//    private readonly IProjectService _projectService;

//    public ProjectsController(IProjectService projectService)
//    {
//        _projectService = projectService;
//    }

//    [HttpPost("create")]
//    public async Task<IActionResult> Create(ProjectDto dto)
//    {
//        var response = await _projectService.CreateProjectAsync(dto);
//        if (!response.Success)
//            return BadRequest(response);
//        return Ok(response);
//    }

//    [HttpGet("getAll")]
//    public async Task<IActionResult> GetAll()
//    {
//        var response = await _projectService.GetAllProjectsAsync();
//        return Ok(response);
//    }

//    [HttpGet("getById/{id}")]
//    public async Task<IActionResult> GetById(int id)
//    {
//        var response = await _projectService.GetProjectByIdAsync(id);
//        if (!response.Success)
//            return NotFound(response);
//        return Ok(response);
//    }

//    [HttpPut("update/{id}")]
//    public async Task<IActionResult> Update(int id, ProjectDto dto)
//    {
//        var response = await _projectService.UpdateProjectAsync(id, dto);
//        if (!response.Success)
//            return NotFound(response);
//        return Ok(response);
//    }

//    [HttpDelete("delete/{id}")]
//    public async Task<IActionResult> Delete(int id)
//    {
//        var response = await _projectService.DeleteProjectAsync(id);
//        if (!response.Success)
//            return NotFound(response);
//        return Ok(response);
//    }
//}




//With CQRS and MediatR
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateProjectCommand command)
    {
        var response = await _mediator.Send(command);
        if (!response.Success)
            return BadRequest(response);
        return Ok(response);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new GetAllProjectsQuery());
        return Ok(response);
    }

    [HttpGet("getById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _mediator.Send(new GetProjectByIdQuery { Id = id });
        if (!response.Success)
            return NotFound(response);
        return Ok(response);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, UpdateProjectCommand command)
    {
        command.Id = id;
        var response = await _mediator.Send(command);
        if (!response.Success)
            return NotFound(response);
        return Ok(response);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _mediator.Send(new DeleteProjectCommand { Id = id });
        if (!response.Success)
            return NotFound(response);
        return Ok(response);
    }
}