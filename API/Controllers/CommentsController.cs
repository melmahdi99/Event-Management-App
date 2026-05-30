using Domain;
using Application.Comment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : BaseApiController<CommentsController>
    {
        private ICommentService _commentService;

        public CommentsController(ICommentService service, ILogger<CommentsController> logger, IMediator mediator) : base(logger, mediator)
        {
            _commentService = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Comment>>> GetComments(Guid activityId)
        {
            var comments = await _commentService.GetComments(activityId);
            return Ok(comments);
        }

        [HttpPost]
        public async Task<ActionResult<ReadCommentDto>> CreateComment(CreateCommentDto c)
        {
            var comment = await _commentService.CreateComment(c);
            return Created("Comment created", comment);
        }
    }
}
