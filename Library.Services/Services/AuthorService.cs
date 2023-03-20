﻿using Library.Repositories.Interfaces;
using Library.Repositories.Models;
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
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;

        public AuthorService(IAuthorRepository repository)
        {
            _repository = repository;
        }
        protected bool ValidateAuthor(AuthorDTO author)
        {
            var validation = true;
            if (author.BirthDate.Year < 100 || author.BirthDate.Year > 2023)
                validation = false;
            if (!char.IsUpper(author.FirstName[0]))
                validation = false;
            if (!char.IsUpper(author.LastName[0]))
                validation = false;
            return validation;
                        
        }
        public async Task<bool> AuthorExists(Guid id)
        {
            return await _repository.AuthorExists(id);
        }

        public async Task<Author> CreateAuthor(AuthorDTO authorToCreate)
        {
            if (!ValidateAuthor(authorToCreate))
                return null;
            var author = new Author
            {
                FirstName = authorToCreate.FirstName,
                LastName = authorToCreate.LastName,
                BirthDate = authorToCreate.BirthDate
            };
            try
            {
                return await _repository.CreateAuthor(author);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteAuthor(Guid id)
        {
            return await _repository.DeleteAuthor(id);
        }

        public async Task<Author> ListAuthor(Guid id)
        {
            return await _repository.ListAuthor(id);
        }

        public async Task<IEnumerable<Author>> ListAuthors()
        {
            return await _repository.ListAuthors();
        }

        public async Task<bool> PutAuthor(Guid id, AuthorDTO authorToPut)
        {
            if (!ValidateAuthor(authorToPut))
                return false;
            var author = new Author
            {
                FirstName = authorToPut.FirstName,
                LastName = authorToPut.LastName,
                BirthDate = authorToPut.BirthDate
            };
            return await _repository.PutAuthor(id, author);
        }

        public async Task<AuthorDTO> ListAuthorOfBook(string title)
        {
            var author =await _repository.ListAuthorOfBook(title);
            var authorDTO = new AuthorDTO()
            {
                BirthDate = author.BirthDate,
                FirstName = author.FirstName,
                LastName = author.LastName
            };
            return authorDTO;
        }
    }
}
