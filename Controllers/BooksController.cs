using Microsoft.AspNetCore.Mvc;
using BookWebApplication.Services;

namespace BookWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] bool hardcoverOnly = false)
        {
            try
            {
                var books = await _bookService.GetBooksAsync(hardcoverOnly);
                return Ok(books);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error fetching books: {ex.Message}");
            }
        }
    }


}
