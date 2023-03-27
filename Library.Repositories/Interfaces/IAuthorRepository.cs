using Library.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<bool> PutAuthor(Guid id, Author author);
        
        Task<bool> DeleteAuthor(Guid id);
        
        Task<Author> ListAuthor(Guid id);
        
        Task<Author> ListAuthorOfBook(string title);
        
        Task<Author> CreateAuthor(Author authorToCreate);
        
        
        Task<IEnumerable<Author>> ListAuthors();
        
        Task<bool> AuthorExists(Guid id);
        
        Task<List<Book>> ListBooksByAuthor(Guid id);
    }
}
