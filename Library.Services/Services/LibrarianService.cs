using Library.Repositories.Interfaces;
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
        ILibrarianRepository _repository;
        public LibrarianService(ILibrarianRepository repository)
        {
            _repository = repository;
        }

        protected bool ValidateLibrarian(LibrarianDTO librarian)
        {
            bool validation = true;
            if (librarian == null)
                validation = false;
            if (!char.IsUpper(librarian.FirstName[0]))
                validation = false;
            if (!char.IsUpper(librarian.LastName[0]))
                validation = false;
            return validation;
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
