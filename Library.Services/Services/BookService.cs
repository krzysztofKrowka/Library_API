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
        public bool DeleteBook(string title)
        {
            return _repository.DeleteBook(title);
        }
        public bool PutBook(string title, BookDTO bookDTO)
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
            return _repository.PutBook(title, book);
        }
        public bool PatchBorrowed(string title, bool isBorrowed)
        {

            return _repository.PatchBorrowed(title, isBorrowed);
        }
        public bool PatchDescription(string title, string description)
        {
            if (!ValidateDescription(description))
                return false;
            return _repository.PatchDescription(title, description);
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
                BookID = Guid.NewGuid(),
                AuthorFirstName = bookDTO.AuthorFirstName,
                AuthorLastName = bookDTO.AuthorLastName,
                Title = bookDTO.Title,
                IsBorrowed = bookDTO.IsBorrowed,
                Category = bookDTO.Category,
                PublicationDate = bookDTO.PublicationDate,
                Description = bookDTO.Description,
            };
            // Validation logic


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
                AuthorFirstName = book.AuthorFirstName,
                AuthorLastName = book.AuthorLastName,
                Category = book.Category,
                PublicationDate = book.PublicationDate,
                IsBorrowed = book.IsBorrowed
            };
        }
        public static IEnumerable<BookDTO> BooksToDTO(IEnumerable<Book> books)
        {
            var result = new List<BookDTO>();
            foreach (var book in books)
                result.Add(BookToDTO(book));
            return result;
        }

        public IEnumerable<Book> ListBooksByAuthor(string FirstName, string LastName)
        {
            return _repository.ListBooksByAuthor(FirstName, LastName);
        }
    }
}
