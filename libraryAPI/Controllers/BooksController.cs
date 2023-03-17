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
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;

        [ActivatorUtilitiesConstructor]
        public BooksController(IBookService bookService)
        {
            _service = bookService;
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


        [HttpGet("ByAuthor/")]
        public ActionResult<IEnumerable<BookDTO>> GetBooksByAuthor(string FirstName, string LastName)
        {
            if (_service.ListBooks() == null)
            {
                return NotFound();
            }

            return Ok(_service.ListBooksByAuthor(FirstName, LastName));
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
        [Authorize(Roles = "Librarian,Assistant")]
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
        [Authorize(Roles = "Librarian,Assistant")]
        public async Task<ActionResult<Book>> PostBook(BookDTO bookDTO)
        {
            Book book = _service.CreateBook(bookDTO);
            if (book == null)
                return BadRequest("Error");
            else
                return Created(nameof(GetBook), bookDTO);
        }


        [HttpPatch("BorrowBook/{title}")]
        [Authorize(Roles = "Librarian,Assistant,Reader")]
        public async Task<IActionResult> BorrowBook(string title)
        {
            var put = _service.PatchBorrowed(title, true);
            if (put)
                return NoContent();
            else
                return NotFound();
        }


        [HttpPatch("ReturnBook/{title}")]
        [Authorize(Roles = "Librarian,Assistant,Reader")]
        public async Task<IActionResult> ReturnBook(string title)
        {
            var put = _service.PatchBorrowed(title, false);
            if (put)
                return NoContent();
            else
                return NotFound();
        }


        [HttpPatch("ChangeDescription/{title}")]
        [Authorize(Roles = "Librarian,Assistant")]
        public async Task<IActionResult> PatchBook(string title, string description)
        {
            var put = _service.PatchDescription(title, description);
            if (put)
                return NoContent();
            else
                return NotFound();
        }


        // DELETE: api/Books/5
        [HttpDelete("{title}")]
        [Authorize(Roles = "Librarian,Assistant")]
        public async Task<IActionResult> DeleteBook(string title)
        {
            var delete = _service.DeleteBook(title);
            if (delete)
                return NoContent();
            else
                return BadRequest();
        }

    }
}





