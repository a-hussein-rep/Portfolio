using System.ComponentModel.DataAnnotations;

namespace TaskHub.API.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public ICollection<TaskItem>? Tasks { get; set; } = new List<TaskItem>();

        public ICollection<Project>? Projects { get; set; } = new List<Project>();
    }
}
