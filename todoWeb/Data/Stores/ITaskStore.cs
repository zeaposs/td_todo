using Microsoft.EntityFrameworkCore;

namespace todoWeb.Data.Stores
{
    public interface ITaskStore
    {
        Task<TaskItem?> GetNextTaskAsync();
        Task<TaskItem?> GetTaskAsync(int id);
        Task<TaskDay?> GetTaskDayAsync(DateOnly dateOnly, bool createNew = true);
        Task<IList<TaskDay>> GetTaskDaysAsync();
        Task RemoveAllTasks(TaskDay taskDay);
        Task RemoveTaskAsync(TaskItem task);
        Task RemoveTaskDayAsync(TaskDay taskDay);
        Task UpdateTaskAsync(TaskItem task);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}