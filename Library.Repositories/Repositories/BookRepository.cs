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
            var authorID = await _context.Authors.Where(a => a.FirstName == FirstName && a.LastName == LastName && !a.IsDeleted).FirstOrDefaultAsync();
            var booksByAuthor =await _context.Books.Where(b => b.AuthorID == authorID.ID && !b.IsDeleted).ToListAsync();
            
            return booksByAuthor;
        }
        public async Task<IEnumerable<Book>> ListBooks()
        {
            return await _context.Books.Where(b => !b.IsDeleted).ToListAsync();
        }
        public async Task<Book> ListBook(string title)
        {
            return await _context.Books.Where(b => b.Title == title && !b.IsDeleted).FirstAsync();
        }
        public async Task<Book> CreateBook(Book book)
        {
            var exists = _context.Books.Any(b => 
                    b.AuthorFirstName == book.AuthorFirstName && 
                    b.AuthorLastName == book.AuthorLastName && 
                    b.Title == book.Title && 
                    b.PublicationDate == book.PublicationDate && 
                    b.Description == book.Description && 
                    b.Category == book.Category);

            if (exists)
            {
                var oldBook = await _context.Books.Where(b =>
                    b.AuthorFirstName == book.AuthorFirstName &&
                    b.AuthorLastName == book.AuthorLastName &&
                    b.Title == book.Title &&
                    b.PublicationDate == book.PublicationDate &&
                    b.Description == book.Description &&
                    b.Category == book.Category).FirstAsync();
                oldBook.IsDeleted = false;
                
                await _context.SaveChangesAsync();

                return oldBook;
            }

            var author = _context.Authors.Where(a => a.FirstName == book.AuthorFirstName && a.LastName == book.AuthorLastName).First();
            var authorID = author.ID;

            book.AuthorID = authorID;
                
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
            book.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
        public bool BookExists(string title) {
            return  _context.Books.Any(e => e.Title == title);
        }

    }
}
