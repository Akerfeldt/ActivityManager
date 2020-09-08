using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityManager.Models;
using ActivityManager.Services.Caches;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ActivityManager.Pages.Activities
{
    public class IndexModel : PageModel
    {
        private readonly ILookupsCache _lookupsCache;

        public IndexModel(ILookupsCache lookupsCache)
        {
            _lookupsCache = lookupsCache ?? throw new ArgumentNullException(nameof(lookupsCache));
        }

        public IList<Activity> Activities { get;set; }

        public async Task OnGetAsync()
        {
            var activities = await _lookupsCache.GetActivities();

            Activities = activities.Values.ToList();
        }
    }
}
