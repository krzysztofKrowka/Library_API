using Library.Repositories.Models;

namespace Library.Repositories.Interfaces
{
    public interface IBookRepository
    {
        public bool PatchCostAndDescription(string title, string description, double cost);
        public bool PatchDescription(string title, string description);
        public bool PatchCost(string title, double cost);
        public bool PutBook(string title, Book bookDTO);
        public bool DeleteBook(string title);
        //public BookDTO BookToDTO(Book book);
        public Book ListBook(string title);
        Book CreateBook(Book bookToCreate);
        IEnumerable<Book> ListBooks();
        IEnumerable<Book> ListBooksByAuthor(string FirstName,string LastName);
        public bool BookExists(string title);
    }
}
