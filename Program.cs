var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Used to have swagger
// builder.Services.AddSwaggerGen();
//builder.Services.AddEndpointsApiExplorer();

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
