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
  public async Task<IActionResult> Create(Category category) {
    _context.Categories.Add(category);
    await _context.SaveChangesAsync();

    return Created($"/Categories/{category.Id}", category);
  }

  [HttpPut("Categories/{id}")]
  public async Task<IActionResult> Update(int id, Category inputCategory) {
    var category = await _context.Categories.FindAsync(id);

    if (category is null) {
      return NotFound();
    }

    category.Name = inputCategory.Name;

    await _context.SaveChangesAsync();

    return Ok(category);
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
