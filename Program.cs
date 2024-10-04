using BookWebApplication;
using BookWebApplication.Services;
using BookWebApplication.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ExternalApiSettings>(builder.Configuration.GetSection("ExternalApi"));

builder.Services.AddHttpClient();

builder.Services.AddScoped<IBookService, BookService>();


builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

// By default serve index.html 
app.UseDefaultFiles();

// serves static files like HTML, CSS, JS from wwwroot folder
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
