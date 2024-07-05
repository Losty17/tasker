using System.Collections.Generic;
using System.Text.Json.Serialization;

public class User
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }

    [JsonIgnore]
    public ICollection<Task> Tasks { get; set; } = new List<Task>();

    [JsonIgnore]
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}
