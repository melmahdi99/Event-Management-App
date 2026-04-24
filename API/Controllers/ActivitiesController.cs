
using Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : BaseApiController
    {
        //localhost:3333/api/activities
        //evaluates to Controller name without the word "Controller"

        private readonly IActivityService _activitiesService;
        public ActivitiesController(IActivityService activitiesService)
        {
            _activitiesService = activitiesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetActivities()
        {
            var activities = await _activitiesService.GetActivitiesAsync();
            if (!activities.Any())
            {
                return NotFound();
            }
            return Ok(activities);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetActivityById(Guid id)
        {
            var activity = await _activitiesService.GetActivityAsync(id);
            if(activity == null) return NotFound();
            return Ok(activity);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity activity)
        {
            var id = await _activitiesService.CreateActivity(activity);
            return CreatedAtAction(nameof(GetActivityById), new{id = activity.Id}, activity);
            //return Created("Activity Created", activity);
        }
    }
}
