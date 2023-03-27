using Library.Repositories.Models;
using Library.Services.Models;

namespace Library.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDTO>> ListBooksByAuthor(string FirstName, string LastName);
        Task<bool> PatchDescription(string title, string description);
        Task<bool> PatchBorrowed(string title, bool isBorrowed);
        Task<bool> PutBook(string title, BookDTO bookDTO);
        Task<bool> DeleteBook(string title);
        Task<BookDTO> ListBook(string title);
        Task<BookDTO> CreateBook(BookDTO productToCreate);
        Task<List<BookDTO>> ListBooks();
    }
}
