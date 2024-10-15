using System.ComponentModel.DataAnnotations.Schema;

namespace todoWeb.Data
{
    /// <remarks>
    /// Named class TaskItem instead of Task to avoid conflict with the <see cref="Task">.
    /// </remarks>
    public class TaskItem
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public bool IsCompleted { get; set; }

        public DateOnly TaskDayTaskDate { get; set; }
    }
}
