
using Application.Activity.Queries;
using Application;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : BaseApiController<ActivitiesController>
    {
        //localhost:3333/api/activities
        //evaluates to Controller name without the word "Controller"

        private readonly IActivityService _activitiesService;
        //Manual mapper implementation
        // private readonly IActivityMapper _mapper;

        //AutoMapper
        public ActivitiesController(IActivityService activitiesService, ILogger<ActivitiesController> logger, IMediator mediator) : base(logger, mediator)
        {
            _activitiesService = activitiesService;
        }

        [HttpGet]
        //Should be an <IActionResult> if not using DTOs
        public async Task<ActionResult<IEnumerable<ReadActivityDto>>> GetActivities(CancellationToken ct)
        {
            _logger.LogInformation("Request to get all activities.");
            var activities = await _mediator.Send(new GetActivityList.Query(), ct);
            if (!activities.Any())
            {
                return NotFound();
            }
            return Ok(activities);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FullReadActivityDto>> GetActivityById(Guid id, CancellationToken ct)
        {
            var activity = await _mediator.Send(new GetActivityDetails.Query{Id = id}, ct);
            if(activity == null) return NotFound();
            return Ok(activity);
        }

        [HttpPost]
        public async Task<ActionResult<ReadActivityDto>> CreateActivity(CreateActivityDto dto, CancellationToken ct)
        {
            //String structure needs to be done like this for logs:
            _logger.LogInformation("Create activity {ActivityTitle}", dto.Title);

            var created = await _mediator.Send(new CreateActivity.Command{Activity = dto}, ct);
            _logger.LogInformation("Created activity with ID: {ActivityId}", created.Id);

            return CreatedAtAction(nameof(GetActivityById), new{id = created.Id}, created);
            //return Created("Activity Created", activity);
        }

       

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteActivity(Guid id)
        {
            await _activitiesService.DeleteActivity(id);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateActivity(FullReadActivityDto dto){
            await _activitiesService.UpdateActivity(dto);
            return NoContent();
        }

        [HttpGet("Buffer")]
        public async Task<ActionResult<IEnumerable<ReadActivityDto>>> GetActivityByBuffer([FromQuery]double latitude, [FromQuery]double longitude, [FromQuery]double radius)
        {
            var activities = await _activitiesService.GetActivitiesByBuffer(latitude, longitude,radius);
            if (!activities.Any())
            {
                return NotFound();
            }
            return Ok(activities);
        }
    }
}
