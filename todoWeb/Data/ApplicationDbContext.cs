using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace todoWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of tasks.
        /// </summary>
        public virtual DbSet<TaskItem> Tasks { get; set; } = default!;

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of task days.
        /// </summary>
        public virtual DbSet<TaskDay> TaskDays { get; set; } = default!;
    }
}
