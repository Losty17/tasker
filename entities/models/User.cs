public class User {
  public int Id { get; set; }
  public string? Username { get; set; }
  public string? Password { get; set; }
  public string? Email { get; set; }
  public required ICollection<Task> Tasks { get; set; }
  public required ICollection<Category> Categories { get; set; }
}