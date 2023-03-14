using Library.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using Library.Repositories.Interfaces;
using Library.Repositories.Interfaces;
namespace Library.Repositories.Repositories
{
    public class BookRepository : IBookRepository
    {
        private BookContext _context;
       public BookRepository(BookContext bookContext) 
       {
            _context = bookContext;
       }
        
        public IEnumerable<BookAuthors> ListBookAuthors()
        {
            return _context.BookAuthors.Select(x => x).ToList();
        }
        public IEnumerable<Book> ListBooksByAuthor(int authorID) { 
            List<Book> books = new List<Book>();
            List<BookAuthors> booksByAuthor = _context.BookAuthors.Where(b => b.Author_ID == authorID).ToList();
            foreach(var book in booksByAuthor) { 
                int id = book.Book_ID;
                var correctBook = _context.Books.Where(b => b.Id == id).Single();
                books.Add(correctBook);
            }
            return books;
        }
        public IEnumerable<Book> ListBooks()
        {
            return _context.Books.Select(x => x).ToList();
        }
        public Book ListBook(string title)
        {
            return _context.Books.Where(b => b.Title == title).Single();
        }
        public Book CreateBook(Book book)
        {
            
            try
            {
                book.Id = (_context.Books.Count()) + 1;
                BookAuthors author = new BookAuthors();
                author.Book_ID = book.Id;
                author.Author_ID = book.Author_ID;
                _context.BookAuthors.Add(author);
                _context.Books.Add(book);
                _context.SaveChanges();
                return book;
            }
            catch
            {
                return null;
            }
        }
        public bool PutBook(string title,Book bookDTO)
        {
            if (title != bookDTO.Title)
            {
                return false;
            }
            var book = _context.Books.Where(b => b.Title == title).Single();
            book.Description = bookDTO.Description;
            book.Author_ID = bookDTO.Author_ID;
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
            var book = _context.Books.Where(b => b.Title == title).First();
            if (book == null)
            {
                return false;
            }
            var bookAuthor = _context.BookAuthors.Where(b => b.Book_ID == book.Id).First();
            _context.Books.Remove(book);
            _context.BookAuthors.Remove(bookAuthor);
            _context.SaveChanges();
            return true;
        }
        public bool BookExists(string title) {
            return (_context.Books?.Any(e => e.Title == title)).GetValueOrDefault();
        }

    }
}
