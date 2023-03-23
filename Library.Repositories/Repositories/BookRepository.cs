using Library.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using Library.Repositories.Interfaces;
using Org.BouncyCastle.Asn1.X509;

namespace Library.Repositories.Repositories
{
    public class BookRepository : IBookRepository
    {
       private readonly ILibraryContext _context;
       public BookRepository(ILibraryContext libraryContext) 
       {
            _context = libraryContext;
       }
        
        public async Task<IEnumerable<Book>> ListBooksByAuthor(string FirstName, string LastName) { 
            var books = new List<Book>();
            var authorID = await _context.Authors.Where(x => x.FirstName == FirstName && x.LastName == LastName).FirstOrDefaultAsync();
            var booksByAuthor =await _context.BookAuthors.Where(b => b.Author_ID == authorID.AuthorID).ToListAsync();
            
            foreach(var book in booksByAuthor) 
            { 
                var id = book.Book_ID;
                var correctBook = await _context.Books.Where(b => b.BookID == id).FirstAsync();
                books.Add(correctBook);
            }
            return books;
        }
        public async Task<IEnumerable<Book>> ListBooks()
        {
            return await _context.Books.ToListAsync();
        }
        public async Task<Book> ListBook(string title)
        {
            return await _context.Books.Where(b => b.Title == title).FirstAsync();
        }
        public async Task<Book> CreateBook(Book book)
        {

                var bookAuthor = new BookAuthors();
                
                bookAuthor.ID = Guid.NewGuid();
                bookAuthor.Book_ID = book.BookID;
                
                var author = _context.Authors.Where(a => a.FirstName == book.AuthorFirstName && a.LastName == book.AuthorLastName).First();
                var authorID = author.AuthorID;

                bookAuthor.Author_ID = authorID;
                book.AuthorID = authorID;
                
                _context.BookAuthors.Add(bookAuthor);
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
               
                return book;


        }
        public async Task<bool> PutBook(string title,Book bookDTO)
        {
            if (title != bookDTO.Title)
            {
                return false;
            }
            var book = await _context.Books.Where(b => b.Title == title).FirstAsync();
            book.Description = bookDTO.Description;
            book.AuthorFirstName = bookDTO.AuthorFirstName;
            book.AuthorLastName = bookDTO.AuthorLastName;
            book.Category = bookDTO.Category;
            book.PublicationDate = bookDTO.PublicationDate;
            book.IsBorrowed = bookDTO.IsBorrowed;
            
            await _context.SaveChangesAsync();
            
            
            if (await _context.Books.Where(b => b.Title == title).FirstAsync() == null)
            {
                return false;
            }
            

            return true;
        }
        public async Task<bool> PatchBorrowed(string title,bool isBorrwed)
        {
            var book =await _context.Books.Where(b => b.Title == title).FirstAsync();
            book.IsBorrowed = isBorrwed;
            
            await _context.SaveChangesAsync();
            
            
            if (await _context.Books.Where(b => b.Title == title).FirstAsync() == null)
            {
                return false;
            }

            return true;
        }
        public async Task<bool> PatchDescription(string title, string description)
        {
            var book =await _context.Books.Where(b => b.Title == title).FirstAsync();
            book.Description = description;
            await _context.SaveChangesAsync();


            if (await _context.Books.Where(b => b.Title == title).FirstAsync() == null)
            {
                return false;
            }

            return true;
        }
        public async Task<bool> DeleteBook(string title)
        {
            if (_context.Books == null)
            {
                return false;
            }
            var book =await _context.Books.Where(b => b.Title == title).FirstAsync();
            if (book == null)
            {
                return false;
            }
            var bookAuthor =await _context.BookAuthors.Where(b =>b.Book_ID == book.BookID).FirstAsync();
            _context.Books.Remove(book);
            _context.BookAuthors.Remove(bookAuthor);
            await _context.SaveChangesAsync();
            return true;
        }
        public bool BookExists(string title) {
            return  _context.Books.Any(e => e.Title == title);
        }

    }
}
