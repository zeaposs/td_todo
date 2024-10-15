using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace todoWeb.Data
{
    public class TaskDay
    {
        [Key]
        public DateOnly TaskDate { get; set; }

        public IList<TaskItem> Tasks { get; set; } = [];
    }
}
