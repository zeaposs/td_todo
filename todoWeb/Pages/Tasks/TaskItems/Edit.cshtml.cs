using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using todoWeb.Data.Stores;
using todoWeb.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace todoWeb.Pages.Tasks.TaskItems
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ITaskStore _taskStore;

        public EditModel(ITaskStore taskStore)
        {
            _taskStore = taskStore;
        }

        [Required]
        [BindProperty]
        public string? TaskDescription { get; set; }

        [BindProperty]
        [HiddenInput]
        public int TaskId { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            TaskId = id;
            var task = await _taskStore.GetTaskAsync(TaskId);
            if (task == null)
            {
                return RedirectToPage("../TaskDays/Index");
            }

            TaskDescription = task.Description;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var task = await _taskStore.GetTaskAsync(TaskId);
            if (task != null)
            {
                task.Description = TaskDescription;
                await _taskStore.UpdateTaskAsync(task);
                await _taskStore.SaveChangesAsync();
            }

            return RedirectToPage("../TaskDays/Index");
        }
    }
}
