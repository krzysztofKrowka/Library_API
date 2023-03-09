using LibraryCore.Domain;
using LibraryInfrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibraryInfrastructure.Commands
{
    public class GetBookCommand : Command
    {
        public static async Task<ActionResult<BookDTO>> GetBook(string title,BookContext _context)
        {
            if (_context.Books == null)
            {
                return null;
            }
            var book = await _context.Books.Where(b => b.Title == title).SingleAsync();

            if (book == null)
            {
                return null;
            }

            return BookToDTO(book);
        }
    }
}
