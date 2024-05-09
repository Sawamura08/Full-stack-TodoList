using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace TodoBackend.Models
{
    public class Persons
    {
        //properties
        public int id { get; set; } // propertiessd
        public string first_name { get; set; } = string.Empty;

        public string last_name { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public string username { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;

    }

    public class LoginCredentials
    {
        public int id { get; set; } // properties
        public string username { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;
    }

    public class AddType
    {
        public int typeId { get; set; }

        [Required] public string typeNames { get; set; } = string.Empty;

        [Required] public string color { get; set; } = string.Empty;
    }

    public class AddTask
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
