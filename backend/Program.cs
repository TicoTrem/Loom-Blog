using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.services;
using backend.models;
using MySqlX.XDevAPI.Common;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// When they ask for a blog project db context, use the MySQL connection method
builder.Services.AddDbContext<BlogProjectDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
// When they ask for an IBlogService implementation, give them EfCoreBlogService
builder.Services.AddScoped<IBlogService, EfCoreBlogService>();

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

app.MapGet("/blogpost", async (IBlogService bs) => await bs.GetAllPosts()).WithName("GetAllBlogPosts");

app.MapGet("/blogpost/{id}", async (IBlogService bs, int id) =>
{
    BlogPost? foundBlogPost = await bs.GetBlogPost(id);
    return foundBlogPost == null ? Results.NotFound() : Results.Ok(foundBlogPost);
}).WithName("GetBlogPost");

// TODO: Make sure the person sending this request is authenticated as the author of the post object
app.MapPost("/blogpost", async (IBlogService bs, BlogPostCreateDto post) =>
{
    BlogPost? createdPost = await bs.CreatePost(post);
    // return the route to get the object as well as the object if success, BadRequest if not
    return createdPost == null ? Results.BadRequest() : Results.CreatedAtRoute(routeName: "GetBlogPost", routeValues: new { Id = createdPost.Id }, value: createdPost);
}).WithName("SaveBlogPost");


app.MapPatch("/blogpost/{id}", async (IBlogService bs, int id, BlogPostUpdateDto post) =>
{
    bool bSuccess = await bs.UpdatePost(id, post);
    return bSuccess ? Results.NoContent() : Results.NotFound();
}).WithName("UpdateBlogPost");

app.MapDelete("/blogpost/{id}", async (IBlogService bs, int id) =>
{
    bool bSuccess = await bs.DeletePost(id);
    return bSuccess ? Results.Accepted() : Results.NotFound();
}).WithName("DeleteBlogPost");

app.Run();

// also defines what can be updated
public record BlogPostUpdateDto(string? Content);
// defines which parts of the new blog object can come from the request
public record BlogPostCreateDto(string Content, int AuthorId);


