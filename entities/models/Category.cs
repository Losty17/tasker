using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int UserId { get; set; } // This is the foreign key
    [JsonIgnore]
    public User? User { get; set; }
    [JsonIgnore]
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
}

