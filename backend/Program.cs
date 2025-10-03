using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.services;
using backend.models;
using MySqlX.XDevAPI.Common;
using System.Diagnostics;
using backend.controllers;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// When they ask for a blog project db context, use the MySQL connection method
builder.Services.AddDbContext<BlogProjectDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
// When they ask for an IBlogService implementation, give them EfCoreBlogService
builder.Services.AddScoped<IBlogPostService, EfCoreBlogPostService>();
builder.Services.AddScoped<IAuthorService, EfCoreAuthorService>();

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

// BlogPost
app.MapGet("/blogpost", async (IBlogPostService bs) => await bs.GetAllPosts()).WithName("GetAllBlogPosts");
app.MapGet("/blogpost/{id}", async (IBlogPostService bs, int id) =>
{
    BlogPost? foundBlogPost = await bs.GetBlogPost(id);
    return foundBlogPost == null ? Results.NotFound($"BlogPost with ID {id} was not found.") : Results.Ok(foundBlogPost);
}).WithName("GetBlogPost");
// TODO: Make sure the person sending this request is authenticated as the author of the post object
app.MapPost("/blogpost", async (IBlogPostService bs, BlogPostCreateDto post) =>
{
    BlogPost? createdPost = await bs.CreatePost(post);
    // return the route to get the object as well as the object if success, BadRequest if not
    return createdPost == null ? Results.BadRequest("Failed to create the BlogPost") : Results.CreatedAtRoute(routeName: "GetBlogPost", routeValues: new { Id = createdPost.Id }, value: createdPost);
}).WithName("CreateNewBlogPost");
app.MapPatch("/blogpost/{id}", async (IBlogPostService bs, int id, BlogPostUpdateDto post) =>
{
    BlogPost? updatedPost = await bs.UpdatePost(id, post);
    return updatedPost == null ? Results.NotFound() : Results.NoContent();
}).WithName("UpdateBlogPost");
app.MapDelete("/blogpost/{id}", async (IBlogPostService bs, int id) =>
{
    bool bSuccess = await bs.DeletePost(id);
    return bSuccess ? Results.Accepted() : Results.NotFound();
}).WithName("DeleteBlogPost");


// Author
app.MapGet("/author", async (IAuthorService authServ) =>
{
    ServiceResponse<IEnumerable<Author>> serviceResponse = await authServ.GetAllAuthors();
    return serviceResponse.DefaultHttpResponse();
}).WithName("GetAllAuthors");
app.MapGet("/author/{id}", async (IAuthorService authServ, int id) =>
{
    ServiceResponse<Author> serviceResponse = await authServ.GetAuthor(id);
    return serviceResponse.DefaultHttpResponse();
}).WithName("GetAuthor");

app.MapPost("/author", async (IAuthorService authServ, AuthorCreateDto author) =>
{
    ServiceResponse<Author> serviceResponse = await authServ.CreateAuthor(author);
    if (serviceResponse.ServiceResult == ServiceResult.Success)
    {
        Author? newAuthor = serviceResponse.Entity;
        if (newAuthor != null)
        {
            return Results.CreatedAtRoute(routeName: "GetAuthor", routeValues: new { Id = newAuthor.Id });
        }
        return Results.InternalServerError("Failed to return Author after creation");
    }
    return Results.BadRequest("Failed to create the Author");
}).WithName("CreateNewAuthor");

app.MapPatch("/author/{id}", async (IAuthorService authServ, int id, AuthorUpdateDto author) =>
{
    ServiceResponse<Author> serviceResponse = await authServ.UpdateAuthor(id, author);
    return serviceResponse.DefaultHttpResponse();
}).WithName("UpdateAuthor");

app.MapDelete("/author/{id}", async (IAuthorService authServ, int id) =>
{
    ServiceResponse<Author> serviceResponse = await authServ.DeleteAuthor(id);
    serviceResponse.DefaultHttpResponse();
}).WithName("DeleteAuthor");

app.Run();

// also defines what can be updated
public record BlogPostUpdateDto(string? Content);
// defines which parts of the new blog object can come from the request
public record BlogPostCreateDto(string Content, int AuthorId);

public record AuthorCreateDto(string Name);
public record AuthorUpdateDto(string? Name);




