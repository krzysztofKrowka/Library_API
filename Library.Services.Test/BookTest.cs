using Moq;
using Library.Services.Interfaces;
using Library.Services.Models;
using Library.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Library.Services.Test
{
    public class BookTest
    {
        private readonly Mock<IBookService> bookService;
        public BookTest() 
        { 
            bookService = new Mock<IBookService>();
        }

        
        [Fact]
        public async void GetBookList()
        {
            //arrange
            var bookList = GetBooksData();
            bookService.Setup(x => x.ListBooks(1, 1)).Returns(bookList);
            var booksController = new BooksController(bookService.Object);            
            var booksFromMethod = bookList.Result;

            //act
            var booksResult = await booksController.GetBooks(1,1);
            var booksFromController = (booksResult.Result as OkObjectResult).Value as IEnumerable<BookDTO>;

            
            //assert
            Assert.NotNull(booksFromController);
            Assert.Equal(booksFromMethod.Count(), booksFromController.Count());
            Assert.Equal(booksFromMethod.ToString(), booksFromController.ToString());
            Assert.True(booksFromMethod.Equals(booksFromController));
        
        }

        
        [Fact]
        public async void GetBookByTitle()
        {
            //arrange
            var book = GetBookData();
            var bookFromMethod = book.Result;
            
            bookService.Setup(x => x.ListBook("Pan Tadeusz")).Returns(book);
            
            var booksController = new BooksController(bookService.Object);

            //act
            var booksResult = await booksController.GetBook("Pan Tadeusz");
            var bookFromController = (booksResult.Result as OkObjectResult).Value as BookDTO;
            
            
            //assert
            Assert.NotNull(bookFromController);              
            Assert.Equal(bookFromMethod.ToString(), bookFromController.ToString()); 
            Assert.True(bookFromMethod.Equals(bookFromController));               
        }

        
        [Theory]
        [InlineData("Iskra")]
        public async void CheckBookExistOrNotByTitle(string title)
        {
            //arrange
            var bookList = GetBooksData();
            bookService.Setup(x => x.ListBooks(1,1)).Returns(bookList);
            var booksController = new BooksController(bookService.Object);

            //act
            var booksResult = await booksController.GetBooks(1,1);
            var booksFromController = (booksResult.Result as OkObjectResult).Value as IEnumerable<BookDTO>;
            var expectedTitle = booksFromController.ElementAt(0).Title;

            
            //assert
            Assert.Equal(title, expectedTitle);

        }

        
        [Fact]
        public async void AddBook()
        {
            //arrange
            var bookList = GetBooksData();
            var booksFromMethod = bookList.Result;

            bookService.Setup(x => x.CreateBook(booksFromMethod.ElementAt(2))).
                Returns(Task.FromResult(booksFromMethod.ElementAt(2)));
            
            var booksController = new BooksController(bookService.Object);

            //act
            var bookResult = await booksController.PostBook(booksFromMethod.ElementAt(2));
            var bookFromController = (bookResult.Result as CreatedResult).Value as BookDTO;

            
            //assert
            Assert.NotNull(bookFromController);
            Assert.Equal(booksFromMethod.ElementAt(2).Title, bookFromController.Title);
            Assert.True(booksFromMethod.ElementAt(2).Title == bookFromController.Title);
        
        }
        
        
        private async Task<List<BookDTO>> GetBooksData()
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
                }

            };
            
            return booksData;
        
        }
        
        private async Task<BookDTO> GetBookData()
        {
            
            var bookData = new BookDTO
            {
                AuthorFirstName = "Andrzej",
                AuthorLastName = "Kowalski",
                Description = "Description",
                Title = "Pan Tadeusz",
                Category = "Category",
                PublicationDate = 1990
            };
            
            return bookData;
        
        }
    }
}