using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using todoWeb.Migrations;

namespace todoWeb.Data.Stores
{
    public class TaskStore<TContext> : ITaskStore where TContext : DbContext
    {
        public TaskStore(TContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            Context = context;
        }

        /// <summary>
        /// Gets the database context for this store.
        /// </summary>
        public virtual TContext Context { get; private set; }

        private DbSet<TaskItem> Tasks => Context.Set<TaskItem>();
        private DbSet<TaskDay> TaskDays => Context.Set<TaskDay>();

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await Context.SaveChangesAsync(cancellationToken);

        public async Task<TaskItem?> GetNextTaskAsync() => await Tasks.OrderBy(t => t.TaskDayTaskDate).Where(t => !t.IsCompleted).FirstOrDefaultAsync();

        public async Task<TaskItem?> GetTaskAsync(int id) => await Tasks.SingleOrDefaultAsync(t => t.Id == id);

        public async Task<TaskDay?> GetTaskDayAsync(DateOnly dateOnly, bool createNew = true)
        {
            ArgumentNullException.ThrowIfNull(dateOnly);
            var taskDay = await TaskDays.Include(td => td.Tasks).SingleOrDefaultAsync(td => td.TaskDate.Equals(dateOnly));
            if (taskDay == null && createNew)
            {
                taskDay = new TaskDay() { TaskDate = dateOnly };
                TaskDays.Add(taskDay);
            }

            return await Task.FromResult(taskDay);
        }

        public async Task<IList<TaskDay>> GetTaskDaysAsync()
        {
            // We don't need pagination for this example. So we're just grabbing all items from the DB.
            return await TaskDays.Include(td => td.Tasks).ToListAsync();
        }

        public async Task RemoveAllTasks(TaskDay taskDay)
        {
            ArgumentNullException.ThrowIfNull(taskDay);
            foreach (var task in taskDay.Tasks)
            {
                await RemoveTaskAsync(task);
            }
        }

        public async Task RemoveTaskDayAsync(TaskDay taskDay)
        {
            ArgumentNullException.ThrowIfNull(taskDay);

            TaskDays.Remove(taskDay);

            await Task.CompletedTask;
        }

        public async Task RemoveTaskAsync(TaskItem task)
        {
            ArgumentNullException.ThrowIfNull(task);

            Tasks.Remove(task);

            await Task.CompletedTask;
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            ArgumentNullException.ThrowIfNull(task);

            Tasks.Update(task);

            await Task.CompletedTask;
        }
    }
}
