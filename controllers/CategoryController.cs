using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class CategoryController(TaskerContext context) : Controller {
  private readonly TaskerContext _context = context;

  [Route("Categories")]
  public async Task<IActionResult> Index() {
    var categories = await _context.Categories.ToListAsync();

    return Ok(categories);
  }

  [Route("Categories/{id}")]
  public async Task<IActionResult> Show(int id) {
    if (await _context.Categories.FindAsync(id) is Category category) {
      return Ok(category);
    }

    return NotFound();
  }

  [HttpPost("Categories")]
  public async Task<IActionResult> Create([FromBody] Category category)
  {
      var user = await _context.Users.FindAsync(category.UserId);
      if (user == null)
      {
          return BadRequest(new { message = "User not found" });
      }

      category.User = user;

      _context.Categories.Add(category);
      await _context.SaveChangesAsync();

      // Return the category without causing a cycle
      return CreatedAtAction(nameof(Show), new { id = category.Id }, new { category.Id, category.Name, category.UserId });
  }

  [HttpPut("Categories/{id}")]
  public async Task<IActionResult> Update(int id, [FromBody] Category inputCategory)
  {
      var category = await _context.Categories.FindAsync(id);

      if (category == null)
      {
          return NotFound();
      }

      category.Name = inputCategory.Name;
      category.UserId = inputCategory.UserId;

      await _context.SaveChangesAsync();

      return Ok(new { category.Id, category.Name, category.UserId });
  }

  [HttpDelete("Categories/{id}")]
  public async Task<IActionResult> Delete(int id) {
    var category = await _context.Categories.FindAsync(id);

    if (category is null) {
      return NotFound();
    }

    _context.Categories.Remove(category);
    await _context.SaveChangesAsync();

    return NoContent();
  }
}
