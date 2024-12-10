namespace TaskManager.DTOs
{
    public class TaskListDTO
    {
        public string? Name { get; set; }
        public string? Owner { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
