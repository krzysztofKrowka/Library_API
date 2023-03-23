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
        private readonly ILibraryContext _context;
        public AuthorRepository(ILibraryContext bookContext)
        {
            _context = bookContext;
        }
        public async Task<bool> AuthorExists(Guid id)
        {
            return (await _context.Authors?.AnyAsync(e => e.AuthorID == id));
        }

        public async Task<Author> CreateAuthor(Author authorToCreate)
        {      
                _context.Authors.Add(authorToCreate);
                await _context.SaveChangesAsync();
                return authorToCreate;
        }

        public async Task<bool> DeleteAuthor(Guid id)
        {
            if (_context.Authors == null)
            {
                return false;
            }
            
            var author =await _context.Authors.Where(b => b.AuthorID == id).FirstAsync();
            
            if (author == null)
            {
                return false;
            }
            
            var books =await _context.Books.Where(b => b.AuthorFirstName == author.FirstName && b.AuthorLastName == author.LastName).ToListAsync();
            var bookAuthors =await _context.BookAuthors.Where(b => b.Author_ID == id).ToListAsync();
            
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
            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<Author> ListAuthor(Guid id)
        {
            return await _context.Authors.Where(b => b.AuthorID == id).FirstAsync();
        }

        public async Task<Author> ListAuthorOfBook(string title)
        {
            var book = await _context.Books.Where(a => a.Title == title).FirstAsync();
            var firstName = book.AuthorFirstName;
            var lastName = book.AuthorLastName;
            var author =await _context.Authors.Where(a => 
                a.FirstName == firstName && 
                a.LastName == lastName).FirstAsync();
            
            return author;
        }
        
        public async Task<IEnumerable<Author>> ListAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<bool> PutAuthor(Guid id, Author author)
        {
            if (id != author.AuthorID)
            {
                return false;
            }
            var authorToPut =await _context.Authors.Where(b => b.AuthorID == id).FirstAsync();
            authorToPut.FirstName = author.FirstName;
            authorToPut.LastName = author.LastName;
            authorToPut.BirthDate = author.BirthDate;

            await _context.SaveChangesAsync();

            if (await _context.Authors.Where(b => b.AuthorID == id).FirstAsync() == null)
            {
                return false;
            }
                else
            
            return true;
        }
        
        public async Task<List<Book>> ListBooksByAuthor(Guid id)
        {
            return await _context.Books.Where(x => x.AuthorID == id).ToListAsync();
        }
    }
}

