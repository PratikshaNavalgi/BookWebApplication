using BookWebApplication.Models;
using BookWebApplication.Services;
using BookWebApplication;
using Microsoft.Extensions.Options;

public class BookService : IBookService
{
    private readonly ExternalApiSettings _externalApiSettings;
    private readonly IHttpClientFactory _clientFactory;

    public BookService(IOptions<ExternalApiSettings> externalApiSettings, IHttpClientFactory clientFactory)
    {
        _externalApiSettings = externalApiSettings.Value;
        _clientFactory = clientFactory;
    }

    public async Task<List<Owner>> GetBooksAsync(bool hardcoverOnly)
    {
        var client = _clientFactory.CreateClient();
        var response = await client.GetAsync(_externalApiSettings.BooksUrl);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to fetch books, status code: {response.StatusCode}");
        }

        var data = await response.Content.ReadFromJsonAsync<List<Owner>>();

        // Apply filtering logic
        return data.Select(o => new Owner
        {
            Name = o.Name,
            Age = o.Age,
            Books = hardcoverOnly
                ? o.Books.Where(b => b.Type == "Hardcover").OrderBy(b => b.Name).ToList()
                : o.Books.OrderBy(b => b.Name).ToList()
        }).ToList();
    }
}
