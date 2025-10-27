using backend.models;
using backend.services;
using Microsoft.AspNetCore.Mvc;
using backend.dtos;

namespace backend.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            ServiceResponse<IEnumerable<Author>> serviceResponse = await _authorService.GetAllAuthors();
            return serviceResponse.DefaultHttpResponse();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            ServiceResponse<Author> serviceResponse = await _authorService.GetAuthor(id);
            return serviceResponse.DefaultHttpResponse();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorCreateDto author)
        {
            ServiceResponse<Author> serviceResponse = await _authorService.CreateAuthor(author);
            if (serviceResponse.ServiceResult == ServiceResult.Success)
            {
                Author? newAuthor = serviceResponse.Entity;
                if (newAuthor != null)
                {
                    return CreatedAtRoute(routeName: "GetAuthor", value: new { id = newAuthor.Id });
                }
                return StatusCode(500, new { message = "Failed to return Author after creation" });
            }
            return BadRequest("Failed to create the Author");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorUpdateDto author)
        {
            ServiceResponse<Author> serviceResponse = await _authorService.UpdateAuthor(id, author);
            return serviceResponse.DefaultHttpResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            ServiceResponse<Author> serviceResponse = await _authorService.DeleteAuthor(id);
            return serviceResponse.DefaultHttpResponse();
        }
    }
}
