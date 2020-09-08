using System;
using System.Threading.Tasks;
using ActivityManager.Models;
using ActivityManager.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ActivityManager.Pages.Activities
{
    public class CreateModel : PageModel
    {
        private readonly IMediator _mediator;

        public CreateModel(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public IActionResult OnGet()
        {
            NewActivityInfo = new NewActivityInfo();

            return Page();
        }

        [BindProperty]
        public NewActivityInfo NewActivityInfo { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _mediator.Send(new CreateActivity
            {
                User = User,
                NewActivityInfo = NewActivityInfo
            });

            return RedirectToPage("./Index");
        }
    }
}
