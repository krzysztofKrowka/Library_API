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
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.Services.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;

        public AuthorService(IAuthorRepository repository)
        {
            _repository = repository;
        }
        
        
        
        protected bool ValidateAuthor(Author author)
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

        public async Task<AuthorDTO> CreateAuthor(string firstName,string lastName,DateTime birthDate)
        {
            
            var author = new Author
            {
                ID = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate,
                IsDeleted = false
            };
            
            if (!ValidateAuthor(author))
                return null;

            return AuthorToDTO(await _repository.CreateAuthor(author));
        
        }

        public async Task<bool> DeleteAuthor(Guid id)
        {
            return await _repository.DeleteAuthor(id);
        }

        public async Task<AuthorDTO> ListAuthor(Guid id)
        {
            
            var author = AuthorToDTO(await _repository.ListAuthor(id));
            author.Books = BookService.BooksToDTO(await _repository.ListBooksByAuthor(id));
            
            return author;
        
        }

        public async Task<IEnumerable<AuthorDTO>> ListAuthors(int pageSize, int pageNumber)
        {
            var authors = await _repository.ListAuthors();
            var authorsDTO = AuthorsToDTO(authors.Skip((pageNumber - 1) * pageSize).Take(pageSize));
            
            foreach(var author in authorsDTO)
                author.Books= BookService.BooksToDTO(await _repository.ListBooksByAuthor(author.AuthorID));
            
            return authorsDTO;
        
        }

        public async Task<bool> PutAuthor(Guid id, AuthorDTO authorToPut)
        {
           
            var author = new Author
            {
                FirstName = authorToPut.FirstName,
                LastName = authorToPut.LastName,
                BirthDate = authorToPut.BirthDate
            }; 
            
            if (!ValidateAuthor(author))
                return false;
            
            return await _repository.PutAuthor(id, author);
        
        }

        public async Task<AuthorDTO> ListAuthorOfBook(string title)
        {
            
            var author =await _repository.ListAuthorOfBook(title);
            
            var authorDTO = new AuthorDTO()
            {
                AuthorID = author.ID,
                BirthDate = author.BirthDate,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Books = BookService.BooksToDTO(await _repository.ListBooksByAuthor(author.ID))
            };
            
            return authorDTO;
        }

        
        public static AuthorDTO AuthorToDTO(Author author)
        {
            
            return new AuthorDTO
            {
                AuthorID = author.ID,
                FirstName = author.FirstName,
                LastName = author.LastName,
                BirthDate = author.BirthDate
            };
        
        }
        
        public static IEnumerable<AuthorDTO>AuthorsToDTO(IEnumerable<Author> authors)
        {
            
            var result = new List<AuthorDTO>();
            
            foreach (var author in authors)
                result.Add(AuthorToDTO(author));
            
            return result;
        
        }
    }
}
