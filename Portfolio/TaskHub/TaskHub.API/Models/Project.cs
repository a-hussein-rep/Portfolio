using System.ComponentModel.DataAnnotations;

namespace TaskHub.API.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
