using Library.Repositories.Interfaces;
using Library.Repositories.Models;
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
        public Librarian CreateLibrarian(Librarian librarian)
        {
            try
            {
                _context.Librarians.Add(librarian);
                _context.SaveChanges();
                return librarian;
            }
            catch
            {
                return null;
            }
        }

        public bool DeleteLibrarian(Guid librarianID)
        {
            var librarian = _context.Librarians.Where(b => b.LibrarianID == librarianID).Single();
            try
            {
                _context.Librarians.Remove(librarian);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Librarian ListLibrarian(Guid librarianID)
        {
            return _context.Librarians.Where(b => b.LibrarianID == librarianID).Single();
        }

        public IEnumerable<Librarian> ListLibrarians()
        {
            return _context.Librarians.Select(x => x).ToList();
        }
    }
}
