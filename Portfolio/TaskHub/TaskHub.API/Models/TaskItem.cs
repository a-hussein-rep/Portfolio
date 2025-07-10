using System.ComponentModel.DataAnnotations;

namespace TaskHub.API.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public int AssignedUserId { get; set; }

        public User AssignedUser { get; set; }
    }
}
