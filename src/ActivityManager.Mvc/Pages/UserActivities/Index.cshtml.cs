using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityManager.Models;
using ActivityManager.Services;
using ActivityManager.Services.Caches;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ActivityManager.Pages.UserActivities
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly ILookupsCache _lookupsCache;

        public IndexModel(IMediator mediator, ILookupsCache lookupsCache)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _lookupsCache = lookupsCache ?? throw new ArgumentNullException(nameof(lookupsCache));
        }

        public IList<Activity> Activities { get;set; }
        public bool ShowCreateButton { get; set; }

        public async Task OnGetAsync()
        {
            var userActivities = await _mediator.Send(new GetUserActivities
            {
                User = HttpContext.User
            });
            Activities = userActivities.Select(x => x.Activity).ToList();

            var activities = await _lookupsCache.GetActivities();

            // hide the "create" button if there are no activities left to sign up for
            ShowCreateButton = !(activities.Count == Activities.Count);
        }
    }
}
