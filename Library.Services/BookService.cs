using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Repositories.Models;
using Library.Repositories.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;
//using System.Web.Mvc;
namespace Library.Services
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
            if(bookToValidate == null) 
                return false;
            if (_repository.BookExists(bookToValidate.Title))
                _modelState.AddModelError("Book", "Book with that title is already added");
            if (bookToValidate.PublicationDate < 200 || bookToValidate.PublicationDate > 2024)
                _modelState.AddModelError("Publication Date", "Publication date is not correct");
            if (bookToValidate.Title.Trim().Length == 0)
                _modelState.AddModelError("Title", "Title is required.");
            if (!char.IsUpper(bookToValidate.Title.Trim()[0]))
                _modelState.AddModelError("Title", "Title's first letter must be upper.");
            if (bookToValidate.Author.Trim().Length == 0)
                _modelState.AddModelError("Author", "Author is required.");
            if (!char.IsUpper(bookToValidate.Author.Trim()[0]))
                _modelState.AddModelError("Author", "Author's name must be upper.");
            if (bookToValidate.Description.Trim().Length <= 50)
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
            if(!ValidateBook(bookDTO))
                return false;
            return _repository.PutBook(title, bookDTO);
        }
        public bool PatchCost(string title, double cost)
        {
            if(!ValidateCost(cost))
                return false;
            return _repository.PatchCost(title, cost);
        }
        public bool PatchDescription(string title, string description)
        {
            if(!ValidateDescription(description))
                return false;
            return _repository.PatchDescription(title, description);
        }
        public bool PatchCostAndDescription(string title, string description,double cost)
        {
            if (!ValidateCost(cost))
                return false;
            if (!ValidateDescription(description))
                return false;
            return _repository.PatchCostAndDescription(title, description, cost);
        }
        public IEnumerable<BookDTO> ListBooks()
        {
            return _repository.ListBooks();
        }
        public BookDTO ListBook(string title)
        {
            return _repository.ListBook(title);
        }
        public Book CreateBook(BookDTO bookToCreate)
        {
            Book book=new Book();
            // Validation logic
            if (!ValidateBook(bookToCreate))
                return null;

            // Database logic
            try
            {
                 book = _repository.CreateBook(bookToCreate);
            }
            catch
            { 
                return null;
            }
            return book;
           
        }
 
    }

    public interface IBookService
    {
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
