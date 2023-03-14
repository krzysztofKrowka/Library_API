using Library.Repositories.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Library.Repositories.Interfaces;
using Library.Services.Interfaces;
using Microsoft.CodeAnalysis;

//using System.Web.Mvc;
namespace Library.Services.Models
{
    public class BookService : IBookService
    {
        
        private ModelStateDictionary _modelState;
        private IBookRepository _repository;

        public BookService(ModelStateDictionary modelState, IBookRepository repository)
        {
            _modelState = modelState;
            _repository = repository;
        }

        protected bool ValidateBook(BookDTO bookToValidate)
        {
            if (bookToValidate == null)
                return false;
            if (_repository.BookExists(bookToValidate.Title))
                _modelState.AddModelError("Book", "Book with that title is already added");
            if (bookToValidate.PublicationDate < 200 || bookToValidate.PublicationDate > 2024)
                _modelState.AddModelError("Publication Date", "Publication date is not correct");
            if (bookToValidate.Title.Trim().Length == 0)
                _modelState.AddModelError("Title", "Title is required.");
            if (!char.IsUpper(bookToValidate.Title.Trim()[0]))
                _modelState.AddModelError("Title", "Title's first letter must be upper.");
            if (bookToValidate.Description.Length <= 50)
                _modelState.AddModelError("Description", "Description must be over 50 characters long.");
            if (bookToValidate.Cost < 0)
                _modelState.AddModelError("Cost", "Cost cannot be less than zero.");
            return _modelState.IsValid;
        }
        protected bool ValidateCost(double cost)
        {
            if (cost < 0)
                _modelState.AddModelError("Cost", "Cost cannot be less than zero.");
            return _modelState.IsValid;
        }
        protected bool ValidateDescription(string description)
        {
            if (description.Length <= 50)
                _modelState.AddModelError("Description", "Description must be over 50 characters long.");
            return _modelState.IsValid;
        }
        public bool DeleteBook(string title)
        {
            return _repository.DeleteBook(title);
        }
        public bool PutBook(string title, BookDTO bookDTO)
        {
            var book = new Book
            {
                Author_ID =bookDTO.Author_ID,
                Title = bookDTO.Title,
                Cost = bookDTO.Cost,
                Category = bookDTO.Category,
                PublicationDate = bookDTO.PublicationDate,
                Description = bookDTO.Description,
            };
            if (!ValidateBook(bookDTO))
                return false;
            return _repository.PutBook(title, book);
        }
        public bool PatchCost(string title, double cost)
        {
            if (!ValidateCost(cost))
                return false;
            return _repository.PatchCost(title, cost);
        }
        public bool PatchDescription(string title, string description)
        {
            if (!ValidateDescription(description))
                return false;
            return _repository.PatchDescription(title, description);
        }
        public bool PatchCostAndDescription(string title, string description, double cost)
        {
            if (!ValidateCost(cost))
                return false;
            if (!ValidateDescription(description))
                return false;
            return _repository.PatchCostAndDescription(title, description, cost);
        }
        public IEnumerable<BookDTO> ListBooks()
        {
            return BooksToDTO(_repository.ListBooks());
        }
        public BookDTO ListBook(string title)
        {
            return BookToDTO(_repository.ListBook(title));
        }
        public Book CreateBook(BookDTO bookDTO)
        {
            var book = new Book
            {
                Author_ID = bookDTO.Author_ID,
                Title = bookDTO.Title,
                Cost = bookDTO.Cost,
                Category = bookDTO.Category,
                PublicationDate = bookDTO.PublicationDate,
                Description = bookDTO.Description,
            };
            // Validation logic
          //  if (!ValidateBook(bookDTO))
            //    return null;

            // Database logic
            try
            {
                book = _repository.CreateBook(book);
            }
            catch
            {
                return null;
            }
            return book;

        }
        public static BookDTO BookToDTO(Book book)
        {
            return new BookDTO
            {
                Title = book.Title,
                Description = book.Description,
                Author_ID = book.Author_ID,
                Category = book.Category,
                PublicationDate = book.PublicationDate,
                Cost = book.Cost
            };
        }
        public static IEnumerable<BookDTO> BooksToDTO(IEnumerable<Book> books)
        {
            List<BookDTO> result = new List<BookDTO>();
            foreach (var book in books)
                result.Add(BookToDTO(book));
            return result;
        }

        public IEnumerable<Book> ListBooksByAuthor(int authorID)
        {
            return _repository.ListBooksByAuthor(authorID);
        }
    
        public IEnumerable<BookAuthors> BookAuthors()
        {
            return _repository.ListBookAuthors();
        }
    }
}
