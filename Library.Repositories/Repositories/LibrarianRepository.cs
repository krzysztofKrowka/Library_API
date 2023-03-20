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
        ILibraryContext _context;
        public LibrarianRepository(ILibraryContext context)
        {
            _context = context;
        }
        public async Task<Librarian> CreateLibrarian(Librarian librarian)
        {
            try
            {
                _context.Librarians.Add(librarian);
                await _context.SaveChangesAsync();
                return librarian;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteLibrarian(Guid librarianID)
        {
            var librarian =await _context.Librarians.Where(b => b.LibrarianID == librarianID).SingleAsync();
            try
            {
                _context.Librarians.Remove(librarian);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Librarian> ListLibrarian(Guid librarianID)
        {
            return await _context.Librarians.Where(b => b.LibrarianID == librarianID).SingleAsync();
        }

        public async Task<IEnumerable<Librarian>> ListLibrarians()
        {
            return await _context.Librarians.ToListAsync();
        }
    }
}
