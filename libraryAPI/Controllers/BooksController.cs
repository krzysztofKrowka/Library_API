using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryCore.Domain;
using Microsoft.Extensions.Hosting;
using LibraryInfrastructure.DTO;
using LibraryInfrastructure.Commands;

namespace libraryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookContext _context;

        public BooksController(BookContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAllBooks()
        {
            if(await GetAllBooksCommand.GetBooks(_context)!= null)
                return Ok(await GetAllBooksCommand.GetBooks(_context));
            else
                return NotFound();
                
        }

        // GET: api/Books/5
        [HttpGet("{title}")]
        public async Task<ActionResult<BookDTO>> GetOneBook(string title)
        {
            if (await GetBookCommand.GetBook(title,_context) != null)
                return Ok(await GetBookCommand.GetBook(title,_context));
            else
                return NotFound();
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{title}")]
        public async Task<IActionResult> PutBook(string title, BookDTO bookDTO)
        {
            await PutBookCommand.PutBook(title,bookDTO, _context);
            return NoContent();

        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      //  [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookDTO bookDTO)
        {
            await PostBookCommand.PostBook(bookDTO, _context);
            return NoContent();
        }
        [HttpPatch("patchCost/{title}")]
        public async Task<IActionResult> PatchBook(string title, double cost)
        {
            var book = await _context.Books.Where(b => b.Title == title).SingleAsync();
            book.Cost = cost;
            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _context.Books.Where(b => b.Title == title).SingleAsync() == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        
        }

        [HttpPatch("patchDescription/{title}")]
        public async Task<IActionResult> PatchBook(string title, string description)
        {
            var book = await _context.Books.Where(b => b.Title == title).SingleAsync();
            book.Description = description;
            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _context.Books.Where(b => b.Title == title).SingleAsync() == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }
        [HttpPatch("patchCostAndDescription/{title}")]
        public async Task<IActionResult> PatchBook(string title, double cost,string description)
        {
            var book = await _context.Books.Where(b => b.Title == title).SingleAsync();
            book.Cost = cost;
            book.Description= description;
            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _context.Books.Where(b => b.Title == title).SingleAsync() == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }
        // DELETE: api/Books/5
        [HttpDelete("{title}")]
        public async Task<IActionResult> DeleteBook(string title)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.Where(b => b.Title == title).SingleAsync();
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool BookExists(string title)
        {
            return (_context.Books?.Any(e => e.Title == title)).GetValueOrDefault();
        }
        private static BookDTO BookToDTO(Book book)
        {
            return new BookDTO
            {
                Title = book.Title,
                Description = book.Description,
                Author = book.Author,
                Category = book.Category,
                PublicationDate = book.PublicationDate,
                Cost = book.Cost
            };
        }
    }
}





