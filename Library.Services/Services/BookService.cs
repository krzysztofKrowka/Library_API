﻿using Library.Repositories.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Library.Repositories.Interfaces;
using Library.Services.Interfaces;
using Microsoft.CodeAnalysis;
using Library.Services.Models;
using System.ComponentModel.DataAnnotations;

//using System.Web.Mvc;
namespace Library.Services.Services
{
    public class BookService : IBookService
    {

        private readonly IBookRepository _repository;

        public BookService( IBookRepository repository)
        {
            _repository = repository;
        }

        
        
        
        protected bool ValidateBook(BookDTO bookToValidate)
        {
            
            var validate = true;
            
            if (bookToValidate == null)
                return false;
            
            if (_repository.BookExists(bookToValidate.Title))
                validate = false;
            
            if (bookToValidate.PublicationDate < 200 || bookToValidate.PublicationDate > 2024)
                validate = false;
            
            if (bookToValidate.Title.Trim().Length == 0)
                validate = false;
            
            if (!char.IsUpper(bookToValidate.Title.Trim()[0]))
                validate = false;
            
            if (bookToValidate.Description.Length <= 50)
                validate = false;
            
            return validate;        
        
        }
        
        protected bool ValidateDescription(string description)
        {
            
            if (description.Length <= 50)
                return false;
            
            return true;
        
        }
        
        public async Task<bool> DeleteBook(string title)
        {
            return await _repository.DeleteBook(title);
        }
        
        public async Task<bool> PutBook(string title, BookDTO bookDTO)
        {
            
            var book = new Book
            {
                AuthorFirstName = bookDTO.AuthorFirstName,
                AuthorLastName = bookDTO.AuthorLastName,
                Title = bookDTO.Title,
                IsBorrowed = bookDTO.IsBorrowed,
                Category = bookDTO.Category,
                PublicationDate = bookDTO.PublicationDate,
                Description = bookDTO.Description,
            };
            
            if (!ValidateBook(bookDTO))
                return false;
            
            return await _repository.PutBook(title, book);
        
        }
        
        public async Task<bool> PatchBorrowed(string title, bool isBorrowed)
        {
            return await _repository.PatchBorrowed(title, isBorrowed);
        }
        
        public async Task<bool> PatchDescription(string title, string description)
        {
            
            if (!ValidateDescription(description))
                return false;
            
            return await _repository.PatchDescription(title, description);
        
        }
        
        public async Task<List<BookDTO>> ListBooks(int pageSize, int pageNumber)
        {
            var books = await _repository.ListBooks();
            return BooksToDTO( books.Skip((pageNumber-1)*pageSize).Take(pageSize) );
        }
        
        public async Task<BookDTO> ListBook(string title)
        {
            return BookToDTO(await _repository.ListBook(title));
        }
        
        public async Task<BookDTO> CreateBook(BookDTO bookDTO)
        {
            
            var book = new Book
            {
                ID = Guid.NewGuid(),
                AuthorFirstName = bookDTO.AuthorFirstName,
                AuthorLastName = bookDTO.AuthorLastName,
                Title = bookDTO.Title,
                IsBorrowed = bookDTO.IsBorrowed,
                Category = bookDTO.Category,
                PublicationDate = bookDTO.PublicationDate,
                Description = bookDTO.Description,
                IsDeleted = false
            };
            
            if (!ValidateBook(bookDTO))
                return null;

            await _repository.CreateBook(book);
 
            return bookDTO;

        }
        
        public async Task<IEnumerable<BookDTO>> ListBooksByAuthor(string FirstName, string LastName)
        {
            return BooksToDTO(await _repository.ListBooksByAuthor(FirstName, LastName));
        }
        
        
        public static BookDTO BookToDTO(Book book)
        {
            return new BookDTO
            {
                Title = book.Title,
                Description = book.Description,
                AuthorFirstName = book.AuthorFirstName,
                AuthorLastName = book.AuthorLastName,
                Category = book.Category,
                PublicationDate = book.PublicationDate,
                IsBorrowed = book.IsBorrowed
            };
        }
        
        public static List<BookDTO> BooksToDTO(IEnumerable<Book> books)
        {
            
            var result = new List<BookDTO>();
            
            foreach (var book in books)
                result.Add(BookToDTO(book));
            
            return result;
        
        }

        
    }
}
