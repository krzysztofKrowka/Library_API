using LibraryCore.Domain;
using LibraryInfrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryInfrastructure.Commands;
using System.Threading.Tasks;
namespace LibraryInfrastructure.Commands
{
    public class PostBookCommand : ControllerBase
    {
        public static async Task<ActionResult<Book>> PostBook(BookDTO bookDTO,BookContext _context)
        {
            if (_context.Books == null)
            {
                return null;
            }
            var book = new Book
            {
                Author = bookDTO.Author,
                Title = bookDTO.Title,
                Cost = bookDTO.Cost,
                Category = bookDTO.Category,
                PublicationDate = bookDTO.PublicationDate,
                Description = bookDTO.Description,
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            BookDTO bookDTO1= Command.BookToDTO(book);
            return null;// CreatedAtAction(nameof(GetBookCommand.GetBook), new { id = book.Id }, bookDTO1);
        }
    }

}
