using BookWebApplication.Models;

namespace BookWebApplication.Services
{
    public interface IBookService
    {
        Task<List<Owner>> GetBooksAsync(bool hardcoverOnly);
    }
}
