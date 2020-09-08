using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityManager.Models;
using ActivityManager.Services;
using ActivityManager.Services.Caches;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ActivityManager.Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActivitiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILookupsCache _lookupsCache;

        public ActivitiesController(IMediator mediator, ILookupsCache lookupsCache)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _lookupsCache = lookupsCache ?? throw new ArgumentNullException(nameof(lookupsCache));
        }

        // GET: api/Activities
        [HttpGet]
        public async Task<ActionResult<IList<Activity>>> GetActivities()
        {
            var activities = await _lookupsCache.GetActivities();

            return activities.Values.ToList();
        }

        // GET: api/Activities/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity(int id)
        {
            var activities = await _lookupsCache.GetActivities();

            if (!activities.ContainsKey(id))
            {
                return NotFound();
            }

            return activities[id];
        }

        // PUT: api/Activities/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Activity>> PutActivity(int id, Activity activity)
        {
            return Ok(await _mediator.Send(new UpdateActivity
            {
                Activity = activity,
                Id = id,
                User = User
            }));
        }

        // POST: api/Activities
        [HttpPost]
        public async Task<ActionResult<Activity>> PostActivity(NewActivityInfo newActivityInfo)
        {
            return Ok(await _mediator.Send(new CreateActivity
            {
                NewActivityInfo = newActivityInfo,
                User = User
            }));
        }

        // DELETE: api/Activities/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Activity>> DeleteActivity(int id)
        {
            return Ok(await _mediator.Send(new DeleteActivity
            {
                ActivityId = id,
                User = User
            }));
        }
    }
}
