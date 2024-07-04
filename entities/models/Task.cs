public class Task
{
  public int Id { get; set; }
  public string? Title { get; set; }
  public int Status { get; set; }
  public DateOnly CreatedAt { get; set; }
  public DateOnly? FinishedAt { get; set; }
  public required Category Category { get; set; }
  public required User User { get; set; }
}