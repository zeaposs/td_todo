using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using todoWeb.Data;
using todoWeb.Data.Stores;

namespace todoWeb.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ITaskStore _taskStore;

        public IndexModel(ILogger<IndexModel> logger, ITaskStore taskStore)
        {
            _logger = logger;
            _taskStore = taskStore;
        }

        public TaskItem? CurrentTask { get; private set; }

        public async Task<IActionResult> OnGet()
        {
            CurrentTask = (await _taskStore.GetNextTaskAsync())!;
            return Page();
        }
    }
}
