public class Category {
  public int Id { get; set; }
  public string? Name { get; set; }
  public required ICollection<Task> Tasks { get; set; }
  public required User User { get; set; }
}