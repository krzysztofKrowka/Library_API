using Library.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection;
using Microsoft.Extensions.Options;

namespace Library.Repositories.Repositories
{
    public class BookRepository : IBookRepository
    {
        
        //public static DbContextOptions<BookContext> options = new DbContextOptions<BookContext>();
        
        private static DbContextOptionsBuilder<BookContext> db = new DbContextOptionsBuilder<BookContext>();
        private  BookContext _context= new BookContext(db.Options);
       public BookRepository(BookContext bookContext) 
       {
            _context = bookContext;
       }



        public IEnumerable<BookDTO> ListBooks()
        {
            return _context.Books.Select(x => BookToDTO(x)).ToList();
        }
        public BookDTO ListBook(string title)
        {
            return BookToDTO(_context.Books.Where(b => b.Title == title).Single());
        }
        public Book CreateBook(BookDTO bookDTO)
        {
            var book = new Book
            {
                Author = bookDTO.Author,
                Title = bookDTO.Title,
                Cost = bookDTO.Cost,
                Category = bookDTO.Category,
                PublicationDate = bookDTO.PublicationDate,
                Description = bookDTO.Description,
            };
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return book;
            }
            catch
            {
                return null;
            }
        }
        public bool PutBook(string title,BookDTO bookDTO)
        {
            if (title != bookDTO.Title)
            {
                return false;
            }
            var book = _context.Books.Where(b => b.Title == title).Single();
            book.Description = bookDTO.Description;
            book.Author = bookDTO.Author;
            book.Category = bookDTO.Category;
            book.PublicationDate = bookDTO.PublicationDate;
            book.Cost = bookDTO.Cost;
            _context.Entry(book).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Books.Where(b => b.Title == title).Single() == null)
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }
        public bool PatchCost(string title,double cost)
        {
            var book = _context.Books.Where(b => b.Title == title).Single();
            book.Cost = cost;
            _context.Entry(book).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Books.Where(b => b.Title == title).Single() == null)
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }
        public bool PatchDescription(string title, string description)
        {
            var book = _context.Books.Where(b => b.Title == title).Single();
            book.Description = description;
            _context.Entry(book).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Books.Where(b => b.Title == title).Single() == null)
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }
        public bool PatchCostAndDescription(string title,string description, double cost)
        {
            var book = _context.Books.Where(b => b.Title == title).Single();
            book.Description = description;
            book.Cost = cost;
            _context.Entry(book).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Books.Where(b => b.Title == title).Single() == null)
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }
        public bool DeleteBook(string title)
        {
            if (_context.Books == null)
            {
                return false;
            }
            var book = _context.Books.Where(b => b.Title == title).Single();
            if (book == null)
            {
                return false;
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;
        }
         public BookDTO BookToDTO(Book book)
        {
            return new BookDTO
            {
                Title = book.Title,
                Description = book.Description,
                Author = book.Author,
                Category = book.Category,
                PublicationDate = book.PublicationDate,
                Cost = book.Cost
            };
        }
    }

    public interface IBookRepository
    {
        public bool PatchCostAndDescription(string title, string description,double cost);
        public bool PatchDescription(string title,string description);
        public bool PatchCost(string title, double cost);
        public bool PutBook(string title, BookDTO bookDTO);
        public bool DeleteBook(string title);
        public BookDTO BookToDTO(Book book);
        public BookDTO ListBook(string title);
        Book CreateBook(BookDTO bookToCreate);
        IEnumerable<BookDTO> ListBooks();
    }
    
}
