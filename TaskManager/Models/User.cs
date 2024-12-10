using System.Text.Json.Serialization;

namespace TaskManager.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; } = "User";
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastUpdatedAt { get; set; } = DateTime.Now;
        [JsonIgnore] public virtual ICollection<TaskList>? TaskLists { get; set; } = new List<TaskList>();
    }
}
