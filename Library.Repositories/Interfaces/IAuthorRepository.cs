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
        public bool PutAuthor(Guid id, Author author);
        public bool DeleteAuthor(Guid id);
        public Author ListAuthor(Guid id);
        public Author ListAuthorOfBook(string title);
        Author CreateAuthor(Author authorToCreate);
        IEnumerable<Author> ListAuthors();
        public bool AuthorExists(Guid id);
    }
}
