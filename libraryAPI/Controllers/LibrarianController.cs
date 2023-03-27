using Library.Repositories.Models;
using Library.Repositories.Repositories;
using Library.Services.Interfaces;
using Library.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Library.Services.Services;
using Microsoft.AspNetCore.Authorization;

namespace Library.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LibrarianController : Controller
    {
        private readonly ILibrarianService _service;

        public LibrarianController(ILibrarianService service)
        {
            _service = service;
        }


        [HttpGet]
        [Authorize(Roles = "Librarian")]
        public async Task<ActionResult<IEnumerable<Librarian>>> GetLibrarians()
        {
            if (await _service.ListLibrarians() == null)
            {
                return NotFound();
            }

            return Ok(await _service.ListLibrarians());

        }
        
        
        [HttpGet("{id}")]
        [Authorize(Roles = "Librarian")]
        public async Task<ActionResult<Librarian>> GetLibrarian(Guid id)
        {
            if (await _service.ListLibrarian(id) == null)
            {
                return NotFound();
            }
            var librarian =await _service.ListLibrarian(id);

            if (librarian == null)
            {
                return NotFound();
            }

            return Ok(librarian);
        }
        
        
        [HttpPost]
        [Authorize(Roles = "Librarian")]
        public async Task<ActionResult<Librarian>> PostLibrarian(LibrarianDTO librarianDTO)
        {
            var librarian =await _service.CreateLibrarian(librarianDTO);
            if (librarian == null)
                return BadRequest("Error");
            else
                return Created(nameof(GetLibrarian), librarian);
        }
        
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> DeleteLibrarian(Guid id)
        {
            var delete =await _service.DeleteLibrarian(id);
            if (delete)
                return NoContent();
            else
                return BadRequest();
        }
    }
}
