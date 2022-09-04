using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsProject.Core.Domain.Dtos.Comments;
using NewsProject.Core.Domain.Entities.RequestParameters;
using NewsProject.Core.Domain.Services;
using Newtonsoft.Json;

namespace NewsProject.Endpoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public IActionResult GetLatestNewses([FromQuery] CommentsParameters commentsParameters)
        {
            var data = _commentService.GetComments(commentsParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(data.MetaData));

            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult GetComment(Guid id)
        {
            var comment = _commentService.GetComment(id);
            return Ok(comment);
        }

        [HttpGet("update/{id}")]
        public IActionResult GetCommentforUpdate(Guid id)
        {
            var comment = _commentService.GetComment(id);
            return Ok(comment);
        }

        [HttpPost]
        [Route("api/news/{newsId}/[controller]")]
        public IActionResult AddComment(Guid newsId, [FromBody] CommentCreationDto comment)
        {
            _commentService.CreateComment(newsId, comment);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment(Guid id)
        {
            _commentService.DeleteComment(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateComment(Guid id, [FromBody] CommentUpdateDto comment)
        {
            _commentService.UpdateComment(id, comment);
            return Ok();
        }

        [HttpPut("accept/{id}")]
        public IActionResult AcceptComment(Guid id)
        {
            _commentService.AcceptComment(id);
            return Ok();
        }
    }
}
