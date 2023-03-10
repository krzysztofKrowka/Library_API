using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Library.Repositories.Repositories;
using Library.Repositories.Models;
namespace libraryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _repository;

        [ActivatorUtilitiesConstructor]
        public BooksController(BookContext bookContext):
           this(new BookRepository(bookContext)) {}
        
        public BooksController(IBookRepository repository)
        {
            _repository = repository;
        }


        // GET: api/Books
        [HttpGet]
        public ActionResult<IEnumerable<BookDTO>> GetBooks()
        {
          if (_repository.ListBooks() == null)
          {
              return NotFound();
          }

            return Ok(_repository.ListBooks());
                
        }

        // GET: api/Books/5
        [HttpGet("{title}")]
        public async Task<ActionResult<BookDTO>> GetBook(string title)
        {
          if (_repository.ListBook(title) == null)
          {
              return NotFound();
          }
            var book = _repository.ListBook(title);

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
            var put = _repository.PutBook(title, bookDTO);
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
            Book book = _repository.CreateBook(bookDTO);
            if(book==null)
                return BadRequest();
            else
                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, _repository.BookToDTO(book));
        }
        [HttpPatch("patchCost/{title}")]
        public async Task<IActionResult> PatchBook(string title, double cost)
        {
            var put = _repository.PatchCost(title, cost);
            if (put)
                return NoContent();
            else
                return NotFound();        
        }

        [HttpPatch("patchDescription/{title}")]
        public async Task<IActionResult> PatchBook(string title, string description)
        {
            var put = _repository.PatchDescription(title, description);
            if (put)
                return NoContent();
            else
                return NotFound();
        }
        [HttpPatch("patchCostAndDescription/{title}")]
        public async Task<IActionResult> PatchBook(string title, double cost,string description)
        {
            var put = _repository.PatchCostAndDescription(title,description, cost);
            if (put)
                return NoContent();
            else
                return NotFound();

        }
        // DELETE: api/Books/5
        [HttpDelete("{title}")]
        public async Task<IActionResult> DeleteBook(string title)
        {
            var delete = _repository.DeleteBook(title);
            if(delete)
                return NoContent();
            else
                return BadRequest();
        }
        
    }
}





