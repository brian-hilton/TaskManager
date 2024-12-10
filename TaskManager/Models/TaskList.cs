using System.Text.Json.Serialization;

namespace TaskManager.Models
{
    public class TaskList
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Owner { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;

        // Foreign Key and Navigation property
        public int? UserId { get; set; }
        [JsonIgnore] public User? User { get; set; }
        [JsonIgnore] public ICollection<TaskModel>? TaskModels { get; set; } = new List<TaskModel>();
    }
}
