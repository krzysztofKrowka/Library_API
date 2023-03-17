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
        public bool PutAuthor(Guid id, AuthorDTO author);
        public bool DeleteAuthor(Guid id);
        public Author ListAuthor(Guid id);
        Author CreateAuthor(AuthorDTO authorToCreate);
        IEnumerable<Author> ListAuthors();
        public bool AuthorExists(Guid id);
        public AuthorDTO ListAuthorOfBook(string title);

    }
}
