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
            return (await _context.Authors?.AnyAsync(e => e.ID == id));
        }

        public async Task<Author> CreateAuthor(Author authorToCreate)
        {      
            var exists = _context.Authors.Any(e => e.FirstName == authorToCreate.FirstName && e.LastName == authorToCreate.LastName 
                        && e.BirthDate == authorToCreate.BirthDate);
            
            if (exists)
            {
                var author = await _context.Authors.Where(e => e.FirstName == authorToCreate.FirstName && e.LastName == authorToCreate.LastName 
                        && e.BirthDate == authorToCreate.BirthDate).FirstAsync();
                
                author.IsDeleted = false;
                await _context.SaveChangesAsync();
                
                return author;
            }
            
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
            
            var author =await _context.Authors.Where(b => b.ID == id).FirstAsync();
            
            if (author == null)
            {
                return false;
            }

            author.IsDeleted = true;
            await _context.SaveChangesAsync();
            
            return true;
        
        }

        public async Task<Author> ListAuthor(Guid id)
        {
            return await _context.Authors.Where(a => a.ID == id && !a.IsDeleted).FirstAsync();
        }

        public async Task<Author> ListAuthorOfBook(string title)
        {
            
            var book = await _context.Books.Where(a => a.Title == title).FirstAsync();
            
            var firstName = book.AuthorFirstName;
            var lastName = book.AuthorLastName;
            
            var author =await _context.Authors.Where(a => 
                a.FirstName == firstName && 
                a.LastName == lastName &&
                !a.IsDeleted).FirstAsync();
            
            return author;
        
        }
        
        public async Task<IEnumerable<Author>> ListAuthors()
        {
            return await _context.Authors.Where(a => !a.IsDeleted).ToListAsync();
        }

        public async Task<bool> PutAuthor(Guid id, Author author)
        {
            
            if (id != author.ID)
            {
                return false;
            }
            
            var authorToPut =await _context.Authors.Where(b => b.ID == id).FirstAsync();
            
            authorToPut.FirstName = author.FirstName;
            authorToPut.LastName = author.LastName;
            authorToPut.BirthDate = author.BirthDate;

            await _context.SaveChangesAsync();

            if (await _context.Authors.Where(b => b.ID == id).FirstAsync() == null)
            {
                return false;
            }
            
            return true;
        
        }
        
        public async Task<List<Book>> ListBooksByAuthor(Guid id)
        {
            return await _context.Books.Where(x => x.AuthorID == id).ToListAsync();
        }
    }
}

