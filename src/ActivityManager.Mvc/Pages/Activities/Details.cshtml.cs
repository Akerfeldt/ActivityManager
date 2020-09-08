using System.Threading.Tasks;
using ActivityManager.Models;
using ActivityManager.Services.Caches;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ActivityManager.Pages.Activities
{
    public class DetailsModel : PageModel
    {
        private readonly ILookupsCache _lookupsCache;

        public DetailsModel(ILookupsCache lookupsCache)
        {
            _lookupsCache = lookupsCache;
        }

        public Activity Activity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var activities = await _lookupsCache.GetActivities();

            if (!activities.TryGetValue(id.Value, out var activity))
                return NotFound();

            Activity = activity;

            return Page();
        }
    }
}
