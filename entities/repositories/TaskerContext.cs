using Microsoft.EntityFrameworkCore;

public class TaskerContext : DbContext
{
    public TaskerContext(DbContextOptions<TaskerContext> options) 
        : base(options) { }

    public DbSet<Task> Tasks => Set<Task>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<User> Users => Set<User>();
}
