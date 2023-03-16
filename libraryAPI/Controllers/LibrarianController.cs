using Library.Repositories.Models;
using Library.Repositories.Repositories;
using Library.Services.Interfaces;
using Library.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace libraryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LibrarianController : Controller
    {
        private readonly ILibrarianService _service;

        [ActivatorUtilitiesConstructor]
        public LibrarianController(BookContext bookContext)
        {
            _service = new LibrarianService(this.ModelState, new LibrarianRepository(bookContext));
        }
        [HttpGet]
        public ActionResult<IEnumerable<Librarian>> GetLibrarians()
        {
            if (_service.ListLibrarians() == null)
            {
                return NotFound();
            }

            return Ok(_service.ListLibrarians());

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Librarian>> GetLibrarian(Guid id)
        {
            if (_service.ListLibrarian(id) == null)
            {
                return NotFound();
            }
            var librarian = _service.ListLibrarian(id);

            if (librarian == null)
            {
                return NotFound();
            }

            return Ok(librarian);
        }
        [HttpPost]
        public async Task<ActionResult<Librarian>> PostLibrarian(LibrarianDTO librarianDTO)
        {
            Librarian book = _service.CreateLibrarian(librarianDTO);
            if (book == null)
                return BadRequest("Error");
            else
                return CreatedAtAction(nameof(GetLibrarian), new { id = book.LibrarianID }, librarianDTO);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibrarian(Guid id)
        {
            var delete = _service.DeleteLibrarian(id);
            if (delete)
                return NoContent();
            else
                return BadRequest();
        }
    }
}
