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
            if (bookToValidate.Author.Trim().Length == 0)
                _modelState.AddModelError("Author", "Author is required.");
            if (bookToValidate.Description.Trim().Length == 0)
                _modelState.AddModelError("Description", "Description is required.");
            if (bookToValidate.Cost < 0)
                _modelState.AddModelError("Cost", "Cost cannot be less than zero.");
            return _modelState.IsValid;
        }

        public IEnumerable<BookDTO> ListBooks()
        {
            return _repository.ListBooks();
        }

        public bool CreateBook(BookDTO bookToCreate)
        {
            // Validation logic
            if (!CreateBook(bookToCreate))
                return false;

            // Database logic
            try
            {
                _repository.CreateBook(bookToCreate);
            }
            catch
            {
                return false;
            }
            return true;
        }

    }

    public interface IBookService
    {
        bool CreateBook(BookDTO productToCreate);
        IEnumerable<BookDTO> ListBooks();
    }
}
