using System;
using System.Threading.Tasks;
using ActivityManager.Models;
using ActivityManager.Services;
using ActivityManager.Services.Caches;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ActivityManager.Pages.Activities
{
    public class EditModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly ILookupsCache _lookupsCache;

        public EditModel(IMediator mediator, ILookupsCache lookupsCache)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _lookupsCache = lookupsCache ?? throw new ArgumentNullException(nameof(lookupsCache));
        }

        [BindProperty]
        public Activity Activity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var activities = await _lookupsCache.GetActivities();

            if (!activities.ContainsKey(id.Value))
                return NotFound();

            var activity = activities[id.Value];

            Activity = new Activity
            {
                Id = activity.Id,
                Description = activity.Description
            };

            if (Activity == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
                return Page();

            await _mediator.Send(new UpdateActivity
            {
                Id = id,
                User = User,
                Activity = Activity
            });

            return RedirectToPage("./Index");
        }
    }
}
