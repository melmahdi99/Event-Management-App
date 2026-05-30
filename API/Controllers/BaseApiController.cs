using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController<T> : ControllerBase
    {
        protected readonly ILogger<T> _logger;
        protected readonly IMediator _mediator;
        protected BaseApiController(ILogger<T> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
    }
}
