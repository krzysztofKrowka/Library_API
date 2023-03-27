using Microsoft.AspNetCore.Mvc;
using Library.Repositories.Repositories;
using Library.Repositories.Models;
using Library.Services.Models;
using Library.Services.Interfaces;
using Library.Services.Services;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Library.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _service;

        public AuthorsController(IAuthorService service)
        {
            _service = service;
        }


        [HttpGet]
        [Authorize(Roles = "Librarian,Assistant")]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
        {
            if (await _service.ListAuthors() == null)
            {
                return NotFound();
            }

            return Ok(await _service.ListAuthors());

        }
        
        
        [HttpGet("OfBook/{title}")]
        [Authorize(Roles = "Librarian,Assistant,Reader")]
        public async Task<ActionResult<AuthorDTO>> GetAuthorOfBook(string title)
        {
            if (await _service.ListAuthors() == null)
            {
                return NotFound();
            }

            return Ok(await _service.ListAuthorOfBook(title));
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Librarian,Assistant")]
        public async Task<ActionResult<AuthorDTO>> GetAuthor(Guid id)
        {
            if (await _service.ListAuthor(id) == null)
            {
                return NotFound();
            }
            var author = await _service.ListAuthor(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        
        [HttpPut("{id}")]
        [Authorize(Roles = "Librarian,Assistant")]
        public async Task<IActionResult> PutAuthor(Guid id, AuthorDTO author)
        {
            var put =await _service.PutAuthor(id, author);
            if (put)
                return NoContent();
            else
                return BadRequest();
        }


        [HttpPost]
        [Authorize(Roles = "Librarian,Assistant")]
        public async Task<ActionResult<AuthorDTO>> PostAuthor(string FirstName, string LastName, DateTime BirthDate)
        {
    
            var author = await _service.CreateAuthor(FirstName,LastName,BirthDate);
 
            if (author == null)
                return BadRequest("Error");
            else
                return Created(nameof(GetAuthor), author);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Librarian,Assistant")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var delete = await _service.DeleteAuthor(id);
            if (delete)
                return NoContent();
            else
                return BadRequest();
        }

    }
}





