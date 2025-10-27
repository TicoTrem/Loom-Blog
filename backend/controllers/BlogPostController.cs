using backend.models;
using backend.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend.dtos;

namespace backend.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _bs;
        public BlogPostController(IBlogPostService bs)
        {
            _bs = bs;
        }

        [HttpGet]
        public async Task<IActionResult> GetBlogPosts()
        {
            ServiceResponse<IEnumerable<BlogPost>> serviceResponse = await _bs.GetAllPosts();
            return serviceResponse.DefaultHttpResponse();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogPost(int id)
        {
            ServiceResponse<BlogPost> serviceResponse = await _bs.GetBlogPost(id);
            return serviceResponse.DefaultHttpResponse();
        }

        [HttpPost]
        public async Task<IActionResult> PostBlogPost(BlogPostCreateDto post)
        {
            // return the route to get the object as well as the object if success, BadRequest if not
            ServiceResponse<BlogPost> serviceResponse = await _bs.CreatePost(post);
            if (serviceResponse.ServiceResult == ServiceResult.Success)
            {
                BlogPost? newPost = serviceResponse.Entity;
                if (newPost != null)
                {
                    // supplies the route to get the newly created object
                    return CreatedAtRoute(routeName: "GetBlogPost", value: new { Id = newPost.Id });
                }
                return StatusCode(500, new { message = "Failed to return BlogPost after creation" });
            }
            return BadRequest("Failed to create the BlogPost");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchBlogPost(int id, BlogPostUpdateDto post)
        {
            ServiceResponse<BlogPost> serviceResponse = await _bs.UpdatePost(id, post);
            return serviceResponse.DefaultHttpResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            ServiceResponse<BlogPost> serviceResponse = await _bs.DeletePost(id);
            return serviceResponse.DefaultHttpResponse();
        }
    }
}
