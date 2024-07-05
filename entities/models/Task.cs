using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Task
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
    public int CategoryId { get; set; }  // Foreign key reference
    public int UserId { get; set; }  // Foreign key reference

    [JsonIgnore]
    public Category Category { get; set; } = null!;
    [JsonIgnore]
    public User User { get; set; } = null!;
}
