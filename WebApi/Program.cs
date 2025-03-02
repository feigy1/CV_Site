using GithubService;
using WebApi.CachedService;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGitHubService(options => builder.Configuration.GetSection("GitHubSetting").Bind(options));
builder.Services.AddMemoryCache();
//builder.Services.AddOpenApi();
builder.Services.AddScoped<IGitHubService, GitHubService1>();
builder.Services.Decorate<IGitHubService, CachedGitHubService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
