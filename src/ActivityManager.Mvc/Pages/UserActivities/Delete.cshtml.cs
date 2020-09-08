using System;
using System.Threading.Tasks;
using ActivityManager.Models;
using ActivityManager.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ActivityManager.Pages.UserActivities
{
    public class DeleteModel : PageModel
    {
        private readonly IMediator _mediator;

        public DeleteModel(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [BindProperty]
        public UserActivity UserActivity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            UserActivity = await _mediator.Send(new GetUserActivity
            {
                User = User,
                ActivityId = id.Value
            });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            await _mediator.Send(new DeleteUserActivity
            {
                User = User,
                ActivityId = id.Value
            });

            return RedirectToPage("./Index");
        }
    }
}
