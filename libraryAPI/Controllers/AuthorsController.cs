using Microsoft.AspNetCore.Mvc;
using Library.Repositories.Repositories;
using Library.Repositories.Models;
using Library.Services.Models;
using Library.Services.Interfaces;
namespace libraryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _service;

        [ActivatorUtilitiesConstructor]
        public AuthorsController(BookContext bookContext)
        {
            _service = new AuthorService(this.ModelState, new AuthorRepository(bookContext));
        }


        // GET: api/Books
        [HttpGet]
        public ActionResult<IEnumerable<Author>> GetAuthors()
        {
            if (_service.ListAuthors() == null)
            {
                return NotFound();
            }

            return Ok(_service.ListAuthors());

        }
        [HttpGet("OfBook/{title}")]
        public async Task<ActionResult<Author>> GetAuthorOfBook(string title)
        {
            if (_service.ListAuthors() == null)
            {
                return NotFound();
            }

            return Ok(_service.ListAuthorOfBook(title));
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(Guid id)
        {
            if (_service.ListAuthor(id) == null)
            {
                return NotFound();
            }
            var author = _service.ListAuthor(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(Guid id, AuthorDTO author)
        {
            var put = _service.PutAuthor(id, author);
            if (put)
                return NoContent();
            else
                return BadRequest();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostAuthor(AuthorDTO author)
        {
            Author authorToCreate = _service.CreateAuthor(author);
            if (authorToCreate == null)
                return BadRequest("Error");
            else
                return CreatedAtAction(nameof(GetAuthor), new { id = authorToCreate.AuthorID }, author);
        }
        
        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var delete = _service.DeleteAuthor(id);
            if (delete)
                return NoContent();
            else
                return BadRequest();
        }

    }
}





