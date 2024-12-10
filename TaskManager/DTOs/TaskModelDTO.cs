namespace TaskManager.DTOs
{
    public class TaskModelDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Status { get; set; } = "Incomplete";
        public string Description { get; set; } = string.Empty;
    }
}
