using Library.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Services.Models;
namespace Library.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<bool> PutAuthor(Guid id, AuthorDTO author);
        Task<bool> DeleteAuthor(Guid id);
        Task<Author> ListAuthor(Guid id);
        Task<Author> CreateAuthor(AuthorDTO authorToCreate);
        Task<IEnumerable<Author>> ListAuthors();
        Task<bool> AuthorExists(Guid id);
        Task<AuthorDTO> ListAuthorOfBook(string title);

    }
}
