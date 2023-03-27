using Library.Repositories.Models;

namespace Library.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<bool> PatchDescription(string title, string description);
        
        Task<bool> PatchBorrowed(string title, bool IsBorrowed);
        
        Task<bool> PutBook(string title, Book bookDTO);
        
        Task<bool> DeleteBook(string title);
        
        Task<Book> ListBook(string title);
        
        Task<Book> CreateBook(Book bookToCreate);
        
        Task<IEnumerable<Book>> ListBooks();
        
        Task<IEnumerable<Book>> ListBooksByAuthor(string FirstName,string LastName);
        
        bool BookExists(string title);
    }
}
