using System.ComponentModel.DataAnnotations;

namespace TodoBackend.DTOs.Request
{
    public class PersonsRequest
    {
        // properties
        public string first_name { get; set; } = string.Empty;

        public string last_name { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public string username { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;
    }

    public class LoginRequest
    {
        public string username { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;
    }

    public class AddTypeRequest
    {
        [Required] public string typeNames { get; set; } = string.Empty;

        [Required] public string color { get; set; } = string.Empty;

    }

    public class AddTaskRequest
    {
        [Required] public string title { get; set; } = string.Empty;
        [Required] public int typeId { get; set; }

        public string description { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime dueDate { get; set; }

        public string subTasks { get; set; } = string.Empty;


    }
}
