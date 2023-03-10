using Microsoft.AspNetCore.Mvc;
using Library.Repositories.Repositories;
using Library.Repositories.Models;
using Library.Services.Models;
using Library.Services.Interfaces;
namespace libraryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;
               
        [ActivatorUtilitiesConstructor]
        public BooksController(BookContext bookContext)
        {
            _service = new BookService(this.ModelState, new BookRepository(bookContext));
        }

        [HttpGet("give")]
        public ActionResult<IEnumerable<BookAuthors>> GetAuthorBooks()
        {
            return Ok(_service.BookAuthors());

        }
        // GET: api/Books
        [HttpGet]
        public ActionResult<IEnumerable<BookDTO>> GetBooks()
        {
          if (_service.ListBooks() == null)
          {
              return NotFound();
          }

            return Ok(_service.ListBooks());
                
        }

        [HttpGet("ByAuthor/{id}")]
        public ActionResult<IEnumerable<BookDTO>> GetBooksByAuthor(int id)
        {
            if (_service.ListBooks() == null)
            {
                return NotFound();
            }

            return Ok(_service.ListBooksByAuthor(id));
        }
        // GET: api/Books/5
        [HttpGet("{title}")]
        public async Task<ActionResult<BookDTO>> GetBook(string title)
        {
          if (_service.ListBook(title) == null)
          {
              return NotFound();
          }
            var book = _service.ListBook(title);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{title}")]
        public async Task<IActionResult> PutBook(string title, BookDTO bookDTO)
        {
            var put = _service.PutBook(title, bookDTO);
            if (put)
                return NoContent();
            else
                return BadRequest();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookDTO bookDTO)
        {
            Book book = _service.CreateBook(bookDTO);
            if(book==null)
                return BadRequest("Error");
            else
                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, BookService.BookToDTO(book));
        }
        [HttpPatch("patchCost/{title}")]
        public async Task<IActionResult> PatchBook(string title, double cost)
        {
            var put = _service.PatchCost(title, cost);
            if (put)
                return NoContent();
            else
                return NotFound();        
        }

        [HttpPatch("patchDescription/{title}")]
        public async Task<IActionResult> PatchBook(string title, string description)
        {
            var put = _service.PatchDescription(title, description);
            if (put)
                return NoContent();
            else
                return NotFound();
        }
        [HttpPatch("patchCostAndDescription/{title}")]
        public async Task<IActionResult> PatchBook(string title, double cost,string description)
        {
            var put = _service.PatchCostAndDescription(title,description, cost);
            if (put)
                return NoContent();
            else
                return NotFound();

        }
        // DELETE: api/Books/5
        [HttpDelete("{title}")]
        public async Task<IActionResult> DeleteBook(string title)
        {
            var delete = _service.DeleteBook(title);
            if(delete)
                return NoContent();
            else
                return BadRequest();
        }
        
    }
}





