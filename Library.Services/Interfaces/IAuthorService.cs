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
        Task<AuthorDTO> ListAuthor(Guid id);
        Task<AuthorDTO> CreateAuthor(string fName,string lName, DateTime bDate);
        Task<IEnumerable<AuthorDTO>> ListAuthors();
        Task<bool> AuthorExists(Guid id);
        Task<AuthorDTO> ListAuthorOfBook(string title);

    }
}
