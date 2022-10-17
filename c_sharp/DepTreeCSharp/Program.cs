using DepTreeCSharp.Interfaces;
using DepTreeCSharp.Models;
using DepTreeCSharp.Services;
using DepTreeCSharp.WebClients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection(nameof(ApiSettings)));
builder.Services.AddSingleton<IPlatformsWebClient, PlatformsWebClient>();
builder.Services.AddSingleton<IDependenciesWebClient, DependenciesWebClient>();
builder.Services.AddSingleton<IDependenciesService, DependenciesService>();

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
