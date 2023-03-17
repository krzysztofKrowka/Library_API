using Library.Repositories.Models;
using Library.Services.Models;

namespace Library.Services.Interfaces
{
    public interface IBookService
    {
        IEnumerable<Book> ListBooksByAuthor(string FirstName, string LastName);
        bool PatchDescription(string title, string description);
        bool PatchBorrowed(string title, bool isBorrowed);
        bool PutBook(string title, BookDTO bookDTO);
        bool DeleteBook(string title);
        BookDTO ListBook(string title);
        Book CreateBook(BookDTO productToCreate);
        IEnumerable<BookDTO> ListBooks();
    }
}
