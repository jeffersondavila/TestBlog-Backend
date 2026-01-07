using TestBlog.DTOs;
using TestBlog.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        // GET: api/<BlogController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogDTO>>> Get()
        {
            var blogs = await _blogService.GetBlogs();
            return Ok(blogs);
        }

        // GET api/<BlogController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogDTO>> Get(int id)
        {
            var blog = await _blogService.GetBlog(id);
            return Ok(blog);
        }

        // POST api/<BlogController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BlogDTO value)
        {
            await _blogService.CreateBlog(value);
            return Ok();
        }

        // PUT api/<BlogController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] BlogDTO value)
        {
            await _blogService.UpdateBlog(value);
            return Ok();
        }

        // DELETE api/<BlogController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogService.DeleteBlog(id);
            return Ok();
        }
    }
}
