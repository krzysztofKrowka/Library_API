using LibraryInfrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryCore.Domain;
namespace LibraryInfrastructure.Commands
{
    public class GetAllBooksCommand : Command
    {
        public static async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks(BookContext _context)
        {
            if (_context.Books == null)
            {
                return null;
            }
            return await _context.Books
                .Select(x => BookToDTO(x))
                .ToListAsync();

        }
    }
}
