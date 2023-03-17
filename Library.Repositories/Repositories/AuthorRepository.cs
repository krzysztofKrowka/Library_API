using Library.Repositories.Interfaces;
using Library.Repositories.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryContext _context;
        public AuthorRepository(LibraryContext bookContext)
        {
            _context = bookContext;
        }
        public bool AuthorExists(Guid id)
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

        public bool DeleteAuthor(Guid id)
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
            List<Book> books = _context.Books.Where(b => b.AuthorFirstName == author.FirstName && b.AuthorLastName == author.LastName).ToList();
            List<BookAuthors> bookAuthors = _context.BookAuthors.Where(b => b.Author_ID == id).ToList();
            if (books != null)
            {
                foreach (var book in books)
                {
                    _context.Books.Remove(book);
                }
                foreach(var book in bookAuthors)
                {
                    _context.BookAuthors.Remove(book);
                }
            }
            _context.Authors.Remove(author);
            _context.SaveChanges();
            return true;
        }

        public Author ListAuthor(Guid id)
        {
            return _context.Authors.Where(b => b.AuthorID == id).Single();
        }

        public Author ListAuthorOfBook(string title)
        {
            string firstName = _context.Books.Where(b =>b.Title==title).Single().AuthorFirstName;
            string lastName = _context.Books.Where(b => b.Title == title).Single().AuthorLastName;
            Author author = _context.Authors.Where(a => a.FirstName ==firstName && a.LastName == lastName).Single();
            return author;
        }
        public IEnumerable<Author> ListAuthors()
        {
            return _context.Authors.Select(x => x).ToList();
        }

        public bool PutAuthor(Guid id, Author author)
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
