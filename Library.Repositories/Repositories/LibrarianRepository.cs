using Library.Repositories.Interfaces;
using Library.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.Repositories
{
    public class LibrarianRepository : ILibrarianRepository
    {
        readonly ILibraryContext _context;
        public LibrarianRepository(ILibraryContext context)
        {
            _context = context;
        }
        public async Task<Librarian> CreateLibrarian(Librarian librarian)
        {
                _context.Librarians.Add(librarian);
                await _context.SaveChangesAsync();
                return librarian;
            
        }

        public async Task<bool> DeleteLibrarian(Guid librarianID)
        {
            var librarian =await _context.Librarians.Where(b => b.ID == librarianID).FirstAsync();
                
            librarian.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Librarian> ListLibrarian(Guid librarianID)
        {
            return await _context.Librarians.Where(b => b.ID == librarianID && !b.IsDeleted).FirstAsync();
        }

        public async Task<IEnumerable<Librarian>> ListLibrarians()
        {
            return await _context.Librarians.Where(x => !x.IsDeleted).ToListAsync();
        }
    }
}
