using Microsoft.EntityFrameworkCore;

public class TaskerContext : DbContext
{
    public TaskerContext(DbContextOptions<TaskerContext> options) 
        : base(options) { }

    public DbSet<Task> Tasks { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
}
