using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.services;
using backend.models;
using backend.controllers;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// When they ask for a blog project db context, use the MySQL connection method
builder.Services.AddDbContext<BlogProjectDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
// When they ask for an IBlogService implementation, give them EfCoreBlogService
builder.Services.AddScoped<IBlogPostService, EfCoreBlogPostService>();
builder.Services.AddScoped<IAuthorService, EfCoreAuthorService>();

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();
// dev only
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

// Data Dto's
// defines which parts of the new blog object can come from the request
public record BlogPostCreateDto(string Title, string Content, int AuthorId) : IModel;
// defines what can be updated
public record BlogPostUpdateDto(string? Title, string? Content) : IModel;
public record AuthorCreateDto(string Name) : IModel;
public record AuthorUpdateDto(string? Name) : IModel;




