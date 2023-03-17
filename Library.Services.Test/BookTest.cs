using Library.Services;
using Moq;
using Library.Services.Services;
using Library.Services.Interfaces;
using Library.Repositories.Models;
using Library.Services.Models;
using Library.API.Controllers;

namespace Library.Services.Test
{
    public class BookTest
    {
        private LibraryContext _context = new LibraryContext();
        private readonly Mock<IBookService> bookService;
        public BookTest() 
        { 
            bookService = new Mock<IBookService>();
        }

        [Fact]
        public void GetBookList_BookList()
        {
            //arrange
            var bookList = GetBooksData();
            bookService.Setup(x => x.ListBooks())
                .Returns(bookList);
            var booksController = new BooksController(_context);

            //act
            var booksResult = booksController.GetBooks();
            IEnumerable<BookDTO> books = booksResult.Value;
            //assert
            Assert.NotNull(books);
            Assert.Equal(GetBooksData().Count(), books.Count());
            Assert.Equal(GetBooksData().ToString(), books.ToString());
            Assert.True(bookList.Equals(books));
        }



        private List<BookDTO> GetBooksData()
        {
            List<BookDTO> booksData = new List<BookDTO>
        {
            new BookDTO
            {
                AuthorFirstName = "Jan",
                AuthorLastName = "Nowak",
                Description = "Description",
                Title = "Iskra",
                Category = "Category",
                PublicationDate = 1990
            },
             new BookDTO
            {
                AuthorFirstName = "Andrzej",
                AuthorLastName = "Kowalski",
                Description = "Description",
                Title = "Pan Tadeusz",
                Category = "Category",
                PublicationDate = 1990
            },
             new BookDTO
            {
                AuthorFirstName = "Jan",
                AuthorLastName = "Tolkien",
                Description = "Description",
                Title = "W³adca Pierœcieni",
                Category = "Category",
                PublicationDate = 1990
            },
        };
            return booksData;
        }
    }
}