using System.ComponentModel.DataAnnotations;

namespace TodoBackend.DTOs.Response
{
    public class PersonResponse
    {
        public int id { get; set; } // properties
        public string first_name { get; set; } = string.Empty;

        public string last_name { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public string username { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;
    }


    public class AddTypeResponse
    {
        public int typeId { get; set; }

        [Required] public string typeNames { get; set; }

        [Required] public string color { get; set; }
    }

    public class AddTaskResponse
    {
        public int taskId { get; set; }

        [Required] public string title { get; set; } = string.Empty;
        [Required] public int typeId { get; set; }

        public string description { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime dueDate { get; set; }

        public string subTasks { get; set; } = string.Empty;


    }
}
