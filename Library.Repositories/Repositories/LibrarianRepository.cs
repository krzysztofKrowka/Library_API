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
        BookContext _context;
        public LibrarianRepository(BookContext context)
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
            Librarian librarian = _context.Librarians.Where(b => b.LibrarianID == librarianID).Single();
            try
            {
                _context.Librarians.Add(librarian);
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
