using DDVM_Website.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DDVM_Website.DTOs;
using DDVM_Website.Services.Interfaces;

namespace DDVM_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsDto>>> GetAllNews()
        {
            return Ok(await _newsService.GetAllNewsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NewsDto>> GetNewsById(int id)
        {
            var news = await _newsService.GetNewsByIdAsync(id);
            if (news == null) return NotFound();
            return Ok(news);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<NewsDto>> CreateNews([FromBody] NewsDto newsDto)
        {
            var createdNews = await _newsService.CreateNewsAsync(newsDto);
            return CreatedAtAction(nameof(GetNewsById), new { id = createdNews.NewsId }, createdNews);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNews(int id, [FromBody] NewsDto newsDto)
        {
            if (id != newsDto.Id) return BadRequest();
            var updatedNews = await _newsService.UpdateNewsAsync(id,newsDto);
            if (updatedNews == null) return NotFound();
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNews(int id)
        {
            var success = await _newsService.DeleteNewsAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}