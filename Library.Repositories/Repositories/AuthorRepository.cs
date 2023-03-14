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
    public class AuthorRepository : IAuthorRepository
    {
        private BookContext _context;
        public AuthorRepository(BookContext bookContext)
        {
            _context = bookContext;
        }
        public bool AuthorExists(int id)
        {
            return (_context.Authors?.Any(e => e.AuthorID == id)).GetValueOrDefault();
        }

        public Author CreateAuthor(Author authorToCreate)
        {
            try
            {
                _context.Authors.Add(authorToCreate);
                _context.SaveChanges();
                return authorToCreate;
            }
            catch
            {
                return null;
            }
        }

        public bool DeleteAuthor(int id)
        {
            if (_context.Authors == null)
            {
                return false;
            }
            var author = _context.Authors.Where(b => b.AuthorID == id).Single();
            if (author == null)
            {
                return false;
            }
            _context.Authors.Remove(author);
            _context.SaveChanges();
            return true;
        }

        public Author ListAuthor(int id)
        {
            return _context.Authors.Where(b => b.AuthorID == id).Single();
        }

        public Author ListAuthorOfBook(string title)
        {
            int id = _context.Books.Where(b =>b.Title==title).Single().Author_ID;
            Author author = _context.Authors.Where(a => a.AuthorID ==id).Single();
            return author;
        }
        public IEnumerable<Author> ListAuthors()
        {
            return _context.Authors.Select(x => x).ToList();
        }

        public bool PutAuthor(int id, Author author)
        {
            if (id != author.AuthorID)
            {
                return false;
            }
            var authorToPut = _context.Authors.Where(b => b.AuthorID == id).Single();
            authorToPut.FirstName = author.FirstName;
            authorToPut.LastName = author.LastName;
            authorToPut.BirthDate = author.BirthDate;
            _context.Entry(authorToPut).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Authors.Where(b => b.AuthorID == id).Single() == null)
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }
    }
}
