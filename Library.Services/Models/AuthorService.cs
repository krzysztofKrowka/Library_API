using Library.Repositories.Interfaces;
using Library.Repositories.Models;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Models
{
    public class AuthorService : IAuthorService
    {
        private ModelStateDictionary _modelState;
        private IAuthorRepository _repository;

        public AuthorService(ModelStateDictionary modelState, IAuthorRepository repository)
        {
            _modelState = modelState;
            _repository = repository;
        }
        protected bool ValidateAuthor(Author author)
        {
            if (author.BirthDate.Year < 100 || author.BirthDate.Year > 2023)
                _modelState.AddModelError("Birth Date", "Birth date is wrong");
            if (!char.IsUpper(author.FirstName[0]))
                _modelState.AddModelError("First Name", "First name must start with upper letter");
            if (!char.IsUpper(author.LastName[0]))
                _modelState.AddModelError("Last Name", "Last name must start with upper letter");
            return _modelState.IsValid;
        }
        public bool AuthorExists(int id)
        {
            return _repository.AuthorExists(id);
        }

        public Author CreateAuthor(Author author)
        {
            if (!ValidateAuthor(author))
                return null;
            try
            {
                author = _repository.CreateAuthor(author);
            }
            catch
            {
                return null;
            }
            return author;
        }

        public bool DeleteAuthor(int id)
        {
            return _repository.DeleteAuthor(id);
        }

        public Author ListAuthor(int id)
        {
            return _repository.ListAuthor(id);
        }

        public IEnumerable<Author> ListAuthors()
        {
            return _repository.ListAuthors();
        }

        public bool PutAuthor(int id, Author author)
        {
            if(!ValidateAuthor(author))
                return false;
            return _repository.PutAuthor(id,author);
        }

        public Author ListAuthorOfBook(string title)
        {
            return _repository.ListAuthorOfBook(title);
        }
    }
}
