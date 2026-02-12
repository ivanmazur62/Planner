using Microsoft.AspNetCore.Mvc;
using Planner.Core.Entities;
using Planner.Services.Interfaces;

namespace Planner.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlannerTasksController(IPlannerTaskService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PlannerTask>>> GetAll(CancellationToken cancellationToken)
    {
        var tasks = await service.GetAllAsync(cancellationToken);
        return Ok(tasks);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PlannerTask>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var task = await service.GetByIdAsync(id, cancellationToken);
        if (task == null)
            return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<PlannerTask>> Create([FromBody] PlannerTask? task, CancellationToken cancellationToken)
    {
        if(task == null)
            return BadRequest();
        
        var created = await service.CreateAsync(task, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var task = await service.GetByIdAsync(id, cancellationToken);
        if(task == null)
            return NotFound();
        
        await service.DeleteAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpGet("completed")]
    public async Task<ActionResult<IReadOnlyList<PlannerTask>>> GetCompleted(CancellationToken cancellationToken)
    {
        var tasks = await service.GetCompletedAsync(cancellationToken);
        return Ok(tasks);
    }

    [HttpGet("pending")]
    public async Task<ActionResult<IReadOnlyList<PlannerTask>>> GetPending(CancellationToken cancellationToken)
    {
        var tasks = await service.GetPendingAsync(cancellationToken);
        return Ok(tasks);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PlannerTask>> Update(Guid id, [FromBody] PlannerTask? task, CancellationToken cancellationToken)
    {
        if(task == null)
            return BadRequest();
        
        var existing = await service.GetByIdAsync(id, cancellationToken);
        if (existing == null)
            return NotFound();
        
        task.Id = id;
        var updated = await service.UpdateAsync(task, cancellationToken);
        return Ok(updated);
    }
}