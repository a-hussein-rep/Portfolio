using TaskHub.API.Models;

namespace TaskHub.API.Dtos
{
    public class TaskDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public int AssignedUserId { get; set; }

        public User AssignedUser { get; set; }
    }
}
