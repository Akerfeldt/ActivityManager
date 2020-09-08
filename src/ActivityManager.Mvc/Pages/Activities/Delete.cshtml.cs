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
    public class DeleteModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly ILookupsCache _lookupsCache;

        public DeleteModel(IMediator mediator, ILookupsCache lookupsCache)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _lookupsCache = lookupsCache ?? throw new ArgumentNullException(nameof(lookupsCache));
        }

        public Activity Activity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var activities = await _lookupsCache.GetActivities();
            if (!activities.TryGetValue(id.Value, out var activity))
                return NotFound();

            Activity = activity;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid || !id.HasValue)
                return NotFound();

            await _mediator.Send(new DeleteActivity
            {
                User = User,
                ActivityId = id.Value
            });

            return RedirectToPage("./Index");
        }
    }
}
