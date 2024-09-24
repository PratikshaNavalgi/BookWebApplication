using BookWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        // Mock Data
        private List<Owner> owners = new List<Owner>
    {
        new Owner { Name = "Alice", Age = 25, Books = new List<Book> { new Book { Name = "Book A", Type = "Hardcover" } }},
        new Owner { Name = "Bob", Age = 15, Books = new List<Book> { new Book { Name = "Book B", Type = "Paperback" }, new Book { Name = "Book C", Type = "Hardcover" } }}
    };

        [HttpGet]

        public IActionResult GetBooks([FromQuery] bool hardcoverOnly = false)
        {           
            var adults = owners.Where(o => o.Age >= 18).ToList();
            var children = owners.Where(o => o.Age < 18).ToList();

            if (hardcoverOnly)
            {
                adults.ForEach(a => a.Books = a.Books.Where(b => b.Type == "Hardcover").ToList());
                children.ForEach(c => c.Books = c.Books.Where(b => b.Type == "Hardcover").ToList());
            }

            adults.ForEach(a => a.Books = a.Books.OrderBy(b => b.Name).ToList());
            children.ForEach(c => c.Books = c.Books.OrderBy(b => b.Name).ToList());

            var result = new
            {
                Adults = adults,
                Children = children
            };

            return Ok(result);
        }
    }

}
