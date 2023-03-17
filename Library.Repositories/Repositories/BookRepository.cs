using Library.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using Library.Repositories.Interfaces;
namespace Library.Repositories.Repositories
{
    public class BookRepository : IBookRepository
    {
       private readonly ILibraryContext _context;
       public BookRepository(ILibraryContext libraryContext) 
       {
            _context = libraryContext;
       }
        
        public IEnumerable<Book> ListBooksByAuthor(string FirstName, string LastName) { 
            var books = new List<Book>();
            var authorID = _context.Authors.Where(x => x.FirstName == FirstName && x.LastName == LastName).FirstOrDefault().AuthorID;
            var booksByAuthor = _context.BookAuthors.Where(b => b.Author_ID == authorID).ToList();
            
            foreach(var book in booksByAuthor) 
            { 
                var id = book.Book_ID;
                var correctBook = _context.Books.Where(b => b.BookID == id).Single();
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
                var author = new BookAuthors();
                author.ID = Guid.NewGuid();
                author.Book_ID =book.BookID;
                author.Author_ID = _context.Authors.Where(a => a.FirstName == book.AuthorFirstName && a.LastName == book.AuthorLastName).Single().AuthorID;  
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
            book.AuthorFirstName = bookDTO.AuthorFirstName;
            book.AuthorLastName = bookDTO.AuthorLastName;
            book.Category = bookDTO.Category;
            book.PublicationDate = bookDTO.PublicationDate;
            book.IsBorrowed = bookDTO.IsBorrowed;
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
        public bool PatchBorrowed(string title,bool isBorrwed)
        {
            var book = _context.Books.Where(b => b.Title == title).Single();
            book.IsBorrowed = isBorrwed;
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
            var bookAuthor = _context.BookAuthors.Where(b =>b.Book_ID == book.BookID).First();
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
