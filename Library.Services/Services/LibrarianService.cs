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
            var validation = true;
            if (librarian == null)
                validation = false;
            if (!char.IsUpper(librarian.FirstName[0]))
                validation = false;
            if (!char.IsUpper(librarian.LastName[0]))
                validation = false;
            return validation;
        }
        public async Task<Librarian> CreateLibrarian(LibrarianDTO librarianDTO)
        {
            if (!ValidateLibrarian(librarianDTO))
                return null;
            var librarian = new Librarian
            {
                LibrarianID = Guid.NewGuid(),
                FirstName = librarianDTO.FirstName,
                LastName = librarianDTO.LastName,
            };

            return await _repository.CreateLibrarian(librarian);
        }

        public async Task<bool> DeleteLibrarian(Guid librarianID)
        {
            return await _repository.DeleteLibrarian(librarianID);
        }

        public async Task<Librarian> ListLibrarian(Guid librarianID)
        {
            return await _repository.ListLibrarian(librarianID);
        }

        public async Task<IEnumerable<Librarian>> ListLibrarians()
        {
            return await _repository.ListLibrarians();
        }
    }
}
