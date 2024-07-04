using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class TaskController(TaskerContext context) : Controller
{
  private readonly TaskerContext _context = context;

  [Route("Tasks")]
  public async Task<IActionResult> Index()
  {
    var tasks = await _context.Tasks.ToListAsync();

    return Ok(tasks);
  }

  [Route("Tasks/{id}")]
  public async Task<IActionResult> Show(int id)
  {
    if (await _context.Tasks.FindAsync(id) is Task task)
    {
      return Ok(task);
    }

    return NotFound();
  }

  [HttpPost("Tasks")]
  public async Task<IActionResult> Create(Task task)
  {
    _context.Tasks.Add(task);
    await _context.SaveChangesAsync();

    return Created($"/Tasks/{task.Id}", task);
  }

  [HttpPut("Tasks/{id}")]
  public async Task<IActionResult> Update(int id, Task inputTask)
  {
    var task = await _context.Tasks.FindAsync(id);

    if (task is null)
    {
      return NotFound();
    }

    task.Title = inputTask.Title;

    if (inputTask.Status != task.Status)
    {
      if (inputTask.Status == 1)
      {
        task.FinishedAt = DateOnly.FromDateTime(DateTime.Now);
      }
      else
      {
        task.FinishedAt = null;
      }

      task.Status = inputTask.Status;
    }

    await _context.SaveChangesAsync();

    return NoContent();
  }

  [HttpDelete("Tasks/{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    if (await _context.Tasks.FindAsync(id) is Task task)
    {
      _context.Tasks.Remove(task);
      await _context.SaveChangesAsync();
      return NoContent();
    }

    return NotFound();
  }
}