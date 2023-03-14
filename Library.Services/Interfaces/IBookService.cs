using Library.Repositories.Models;
using Library.Services.Models;

namespace Library.Services.Interfaces
{
    public interface IBookService
    {
        IEnumerable<BookAuthors> BookAuthors();
        IEnumerable<Book> ListBooksByAuthor(int authorID);
        bool PatchCostAndDescription(string title, string description, double cost);
        bool PatchDescription(string title, string description);
        bool PatchCost(string title, double cost);
        bool PutBook(string title, BookDTO bookDTO);
        bool DeleteBook(string title);
        BookDTO ListBook(string title);
        Book CreateBook(BookDTO productToCreate);
        IEnumerable<BookDTO> ListBooks();
    }
}
