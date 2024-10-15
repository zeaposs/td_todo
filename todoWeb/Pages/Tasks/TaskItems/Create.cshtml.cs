using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using todoWeb.Data.Stores;
using todoWeb.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace todoWeb.Pages.Tasks.TaskItems
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ITaskStore _taskStore;

        public CreateModel(ITaskStore taskStore)
        {
            _taskStore = taskStore;
        }

        [Required]
        [BindProperty]
        public string? TaskDescription { get; set; }

        [BindProperty]
        [HiddenInput]
        public string? TaskDayId { get; set; }

        public void OnGet(string id)
        {
            TaskDayId = id;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(TaskDayId))
            {
                return Page();
            }

            var taskDay = (await _taskStore.GetTaskDayAsync(DateOnly.Parse(TaskDayId)))!;

            taskDay.Tasks.Add(new TaskItem() { Description = TaskDescription });
            await _taskStore.SaveChangesAsync();

            return RedirectToPage("../TaskDays/Index");
        }
    }
}
