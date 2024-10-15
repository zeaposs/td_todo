using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using todoWeb.Data;
using todoWeb.Data.Stores;

namespace todoWeb.Pages.Tasks.TaskDays
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ITaskStore _taskStore;

        public IndexModel(ITaskStore taskStore)
        {
            _taskStore = taskStore;
        }

        public IList<TaskDay>? TaskDays { get; set; }

        public async void OnGet()
        {
            TaskDays = await _taskStore.GetTaskDaysAsync();
        }

        public async Task<IActionResult> OnPostDeleteTaskAsync(int id)
        {
            var task = await _taskStore.GetTaskAsync(id);
            if (task != null)
            {
                await _taskStore.RemoveTaskAsync(task);
                await _taskStore.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostToggleTaskAsync(int id)
        {
            var task = await _taskStore.GetTaskAsync(id);
            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
                await _taskStore.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            // TODO: Well, if we want to use DateOnly instead of string, we need to add a custom type converter/model binder.
            var dateOnlyId = DateOnly.Parse(id);
            var taskDay = await _taskStore.GetTaskDayAsync(dateOnlyId, false);

            if (taskDay != null)
            {
                await _taskStore.RemoveTaskDayAsync(taskDay);
                await _taskStore.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
