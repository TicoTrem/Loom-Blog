using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.services;
using backend.models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// When they ask for a blog project db context, use the MySQL connection method
builder.Services.AddDbContext<BlogProjectDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
// When they ask for an IBlogService implementation, give them EfCoreBlogService
builder.Services.AddScoped<IBlogService, EfCoreBlogService>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/blogpost", (IBlogService bs) => bs.GetAllPosts()).WithName("GetAllBlogPosts");

// TODO: Make sure the person sending this request is authenticated as the author of the post object
app.MapPost("/blogpost", (IBlogService bs, BlogPost post) =>
    bs.CreatePost(post)).WithName("SaveBlogPost");

app.MapPatch("/blogpost/{id}", (IBlogService bs, int id, BlogPost post) =>
    bs.UpdatePost(id, post)).WithName("UpdateBlogPost");

app.Run();

record BlogPostRequest(string Content, int authorId);


