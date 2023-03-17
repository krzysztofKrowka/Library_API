using Library.Repositories.Models;
using Library.Repositories.Repositories;
using Library.Services.Interfaces;
using Library.Services.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    public class LibrarianService : ILibrarianService
    {
        LibrarianRepository _repository;
        ModelStateDictionary _modelState;
        public LibrarianService(ModelStateDictionary modelState, LibrarianRepository repository)
        {
            _repository = repository;
            _modelState = modelState;
        }

        protected bool ValidateLibrarian(LibrarianDTO librarian)
        {
            if (librarian == null)
                _modelState.AddModelError("Librarian", "Librarian is null");
            if (!char.IsUpper(librarian.FirstName[0]))
                _modelState.AddModelError("First Name", "First name must start with upper letter");
            if (!char.IsUpper(librarian.LastName[0]))
                _modelState.AddModelError("Last Name", "Last name must start with upper letter");
            return _modelState.IsValid;
        }
        public Librarian CreateLibrarian(LibrarianDTO librarianDTO)
        {
            if (!ValidateLibrarian(librarianDTO))
                return null;
            var librarian = new Librarian
            {
                LibrarianID = Guid.NewGuid(),
                FirstName = librarianDTO.FirstName,
                LastName = librarianDTO.LastName,
            };

            return _repository.CreateLibrarian(librarian);
        }

        public bool DeleteLibrarian(Guid librarianID)
        {
            return _repository.DeleteLibrarian(librarianID);
        }

        public Librarian ListLibrarian(Guid librarianID)
        {
            return _repository.ListLibrarian(librarianID);
        }

        public IEnumerable<Librarian> ListLibrarians()
        {
            return _repository.ListLibrarians();
        }
    }
}
