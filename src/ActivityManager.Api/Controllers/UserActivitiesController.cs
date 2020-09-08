using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActivityManager.Models;
using ActivityManager.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ActivityManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserActivitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserActivitiesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET: api/UserActivities
        [HttpGet]
        public async Task<ActionResult<IList<Activity>>> GetUserActivities()
        {
            return Ok(await _mediator.Send(new GetUserActivities
            {
                User = User
            }));
        }

        // GET: api/UserActivities/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetUserActivity(int id)
        {
            return Ok(await _mediator.Send(new GetUserActivity
            {
                ActivityId = id,
                User = User
            }));
        }

        // POST: api/UserActivities
        [HttpPost]
        public async Task<ActionResult<Activity>> PostUserActivity(NewUserActivityInfo newUserActivityInfo)
        {
            return Ok(await _mediator.Send(new CreateUserActivity
            {
                NewUserActivityInfo = newUserActivityInfo,
                User = User
            }));
        }

        // DELETE: api/UserActivities/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Activity>> DeleteUserActivity(int id)
        {
            return Ok(await _mediator.Send(new DeleteUserActivity
            {
                ActivityId = id,
                User = User
            }));
        }
    }
}
