using Library.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Interfaces
{
    public interface IAuthorService
    {
        public bool PutAuthor(int id, Author author);
        public bool DeleteAuthor(int id);
        public Author ListAuthor(int id);
        Author CreateAuthor(Author authorToCreate);
        IEnumerable<Author> ListAuthors();
        public bool AuthorExists(int id);
        public Author ListAuthorOfBook(string title);

    }
}
