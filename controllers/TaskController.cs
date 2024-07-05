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
    public async Task<IActionResult> Create([FromBody] Task task)
    {
        if (task == null) return BadRequest(new { message = "Task is null" });
        if (task.CategoryId == 0) return BadRequest(new { message = "CategoryId is not set" });
        if (task.UserId == 0) return BadRequest(new { message = "UserId is not set" });

        var category = await _context.Categories.FindAsync(task.CategoryId);
        var user = await _context.Users.FindAsync(task.UserId);

        if (category == null || user == null)
        {
            return BadRequest(new { message = "Category or User not found" });
        }

        task.Category = category;
        task.User = user;
        task.CreatedAt = DateTime.Now;

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Show), new { id = task.Id }, new { task.Id, task.Title, task.Status, task.CreatedAt, task.FinishedAt, CategoryId = task.Category.Id, UserId = task.User.Id });
    }

    [HttpPut("Tasks/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Task inputTask)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        task.Title = inputTask.Title;
        task.Status = inputTask.Status;

        if (inputTask.Status == 1)
        {
            task.FinishedAt = DateTime.Now;
        }
        else
        {
            task.FinishedAt = null;
        }

        var category = await _context.Categories.FindAsync(inputTask.CategoryId);
        var user = await _context.Users.FindAsync(inputTask.UserId);

        if (category == null || user == null)
        {
            return BadRequest(new { message = "Category or User not found" });
        }

        task.Category = category;
        task.User = user;

        await _context.SaveChangesAsync();
        return Ok(new { task.Id, task.Title, task.Status, task.CreatedAt, task.FinishedAt, CategoryId = task.Category.Id, UserId = task.User.Id });
    }

    [HttpDelete("Tasks/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
