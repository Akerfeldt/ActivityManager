using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ActivityManager.Models;
using ActivityManager.Services;
using ActivityManager.Services.Caches;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ActivityManager.Pages.UserActivities
{
    public class CreateModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly ILookupsCache _lookupsCache;

        public CreateModel(IMediator mediator, ILookupsCache lookupsCache)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _lookupsCache = lookupsCache ?? throw new ArgumentNullException(nameof(lookupsCache));
        }

        public async Task<IActionResult> OnGet()
        {
            NewUserActivityInfo = new NewUserActivityInfo();

            return await GetPageWithViewData();
        }

        [BindProperty]
        public NewUserActivityInfo NewUserActivityInfo { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return await GetPageWithViewData();

            await _mediator.Send(new CreateUserActivity
            {
                User = User,
                NewUserActivityInfo = NewUserActivityInfo
            });

            return RedirectToPage("./Index");
        }

        private async Task<PageResult> GetPageWithViewData()
        {
            var userId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var userActivities = await _mediator.Send(new GetUserActivities
            {
                User = User
            });
            var activities = await _lookupsCache.GetActivities();

            var availableActivities = activities.Values.Where(x => !userActivities.Any(y => y.ActivityId == x.Id)).ToList();

            NewUserActivityInfo.ActivityId = availableActivities.First().Id;
            var selectList = new SelectList(availableActivities, "Id", "Description");
            ViewData["Activities"] = selectList;

            return Page();
        }
    }
}
