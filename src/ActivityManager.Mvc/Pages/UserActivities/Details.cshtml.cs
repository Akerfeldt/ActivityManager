using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ActivityManager.Models;
using ActivityManager.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ActivityManager.Pages.UserActivities
{
    public class DetailsModel : PageModel
    {
        private readonly IMediator _mediator;

        public DetailsModel(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public UserActivity UserActivity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

            UserActivity = await _mediator.Send(new GetUserActivity { ActivityId = id.Value, User = User });

            if (UserActivity == null)
                return NotFound();

            return Page();
        }
    }
}
