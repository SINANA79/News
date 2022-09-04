using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsProject.Core.Domain.Dtos.News;
using NewsProject.Core.Domain.Entities.RequestParameters;
using NewsProject.Core.Domain.Services;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace NewsProject.Endpoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _NewsService;

        public NewsController(INewsService newsService)
        {
            _NewsService = newsService;
        }

        [HttpGet]
        public IActionResult GetAllNewses([FromQuery] NewsParameters newsParameters)
        {
            var data = _NewsService.GetAllNewses(newsParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(data.MetaData));

            return Ok(data);
        }

        [HttpGet]
        [Route("/api/category/{categoryId}/newses")]
        public IActionResult GetCategoryNewses(Guid categoryId, [FromQuery] LatestNewsParameters newsParameters)
        {
            var data = _NewsService.GetCategoryNewses(categoryId, newsParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(data.MetaData));
            return Ok(data);
        }

        [HttpGet("mostview")]
        public IActionResult GetMostViewNewses()
        {
            var data = _NewsService.GetLastMostViewNewses();
            return Ok(data);
        }

        [HttpGet("mostcomment")]
        public IActionResult GetMostCommentNewses()
        {
            var data = _NewsService.GetLastMostCommentNewses();
            return Ok(data);
        }

        [HttpGet("alllatest")]
        public IActionResult GetLatestNewses([FromQuery] LatestNewsParameters newsParameters)
        {
            var data = _NewsService.GetLatestNewses(newsParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(data.MetaData));

            return Ok(data);
        }

        [HttpGet("latest")]
        public IActionResult GetLatestNews()
        {
            var data = _NewsService.GetLast8Newses();
            return Ok(data);
        }

        [HttpGet("important")]
        public IActionResult GetImportantNews()
        {
            var data = _NewsService.GetLastImportantNewses();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult GetNews(Guid id)
        {
            var data = _NewsService.GetNews(id);
            return Ok(data);
        }

        [HttpGet("Dependents/{id}")]
        public IActionResult GetNewsWithDependents(Guid id)
        {
            var data = _NewsService.GetNewsWithDependents(id);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult CreateNews([FromBody] NewsCreationDto news)
        {
            _NewsService.CreateNews(news);
            return Ok();
        }

        //[HttpPost("upload")]
        //public IActionResult UploadImage([FromForm] IFormFile newsImage)
        //{
        //    if (newsImage == null || newsImage.Length == 0) return BadRequest("Upload Any Image");
        //    string fileName = newsImage.FileName;
        //    string extension = Path.GetExtension(fileName);
        //    string[] allow = { ".jpg", ".png", ".jpeg" };
        //    if(!allow.Contains(extension.ToLower())) return BadRequest("Invalid Image");
        //    string newFileName = $"{Guid.NewGuid()}{extension}";
        //    string filePath = Path.Combine(_webHostEnvironment.ContentRootPath,
        //        "wwwroot", "Files", newFileName);

        //    using(var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        //    {
        //        newsImage.CopyToAsync(fileStream);
        //    }
        //    return Ok($"Files/{newFileName}");

        //}

        [HttpPost("/upload")]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("wwwroot", "StaticFiles", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(dbPath);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNews(Guid id, [FromBody] NewsUpdateDto news)
        {
            _NewsService.UpdateNews(id, news);
            return Ok();
        }

        [HttpDelete("softdelete/{id}")]
        public IActionResult DeleteNews(Guid id)
        {
            _NewsService.DeleteNews(id);
            return Ok();
        }

    }
}
