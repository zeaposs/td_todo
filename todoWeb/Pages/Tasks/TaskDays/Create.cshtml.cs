using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using todoWeb.Data.Stores;
using todoWeb.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace todoWeb.Pages.Tasks.TaskDays
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
        [DataType(DataType.Date)]
        public DateTime InputDate { get; set; }

        public void OnGet()
        {
            InputDate = DateTime.Now;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO: Check, if this day already exists and return with a warning? Not needed ATM.
            await _taskStore.GetTaskDayAsync(DateOnly.FromDateTime(InputDate));
            await _taskStore.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
