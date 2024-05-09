using System.ComponentModel.DataAnnotations;

namespace TodoBackend.DTOs.Request
{
    public class TaskRequest
    {
        public int taskId { get; set; }

        [Required] public string title { get; set; } = string.Empty;
        [Required] public int typeId { get; set; }

        public string description { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime dueDate { get; set; }

        public string subTasks { get; set; } = string.Empty;
    }

    public class DeleteTaskRequest
    {
        [Required] public int taskId { get; set; }

    }
}
