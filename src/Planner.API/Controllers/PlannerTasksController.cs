using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Planner.API.Models.DTOs;
using Planner.API.Models.Mappings;
using Planner.API.Models.Requests;
using Planner.Core.Entities;
using Planner.Services.Interfaces;

namespace Planner.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PlannerTasksController(IPlannerTaskService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PlannerTaskDto>>> GetAll(CancellationToken cancellationToken)
    {
        var plannerTasks = await service.GetAllAsync(cancellationToken);
        return Ok(plannerTasks.Select(t => t.ToDto()));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PlannerTaskDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var plannerTasks = await service.GetByIdAsync(id, cancellationToken);
        if (plannerTasks == null)
            return NotFound();
        return Ok(plannerTasks.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<PlannerTaskDto>> Create([FromBody] CreatePlannerTaskRequest? request, CancellationToken cancellationToken)
    {
        if(request == null)
            return BadRequest();
        
        var created = await service.CreateAsync(request.ToEntity(), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToDto());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var plannerTasks = await service.GetByIdAsync(id, cancellationToken);
        if(plannerTasks == null)
            return NotFound();
        
        await service.DeleteAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpGet("completed")]
    public async Task<ActionResult<IReadOnlyList<PlannerTaskDto>>> GetCompleted(CancellationToken cancellationToken)
    {
        var plannerTasks = await service.GetCompletedAsync(cancellationToken);
        return Ok(plannerTasks.Select(t => t.ToDto()));
    }

    [HttpGet("pending")]
    public async Task<ActionResult<IReadOnlyList<PlannerTaskDto>>> GetPending(CancellationToken cancellationToken)
    {
        var plannerTasks = await service.GetPendingAsync(cancellationToken);
        return Ok(plannerTasks.Select(t => t.ToDto()));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PlannerTaskDto>> Update(Guid id, [FromBody] UpdatePlannerTaskRequest? request, CancellationToken cancellationToken)
    {
        if(request == null)
            return BadRequest();
        
        var existing = await service.GetByIdAsync(id, cancellationToken);
        if (existing == null)
            return NotFound();

        var plannerTask = new PlannerTask
        {
            Id = id,
            UserId = existing.UserId,
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            IsCompleted = request.IsCompleted,
            CreatedAt = existing.CreatedAt,
        };
        
        var updated = await service.UpdateAsync(plannerTask, cancellationToken);
        return Ok(updated.ToDto());
    }
}