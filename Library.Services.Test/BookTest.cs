using Library.Services;
using Moq;
using Library.Services.Services;
using Library.Services.Interfaces;
using Library.Repositories.Models;
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
        public async void GetBookList_BookList()
        {
            //arrange
            var bookList = GetBooksData();
            bookService.Setup(x => x.ListBooks()).Returns(bookList);
            var booksController = new BooksController(bookService.Object);

            //act
            var booksResult = await booksController.GetBooks();
            var b = (booksResult.Result as OkObjectResult).Value as IEnumerable<BookDTO>;
            //        ERROR
            //  books.Value is 'null'
            
            //assert
            Assert.NotNull(b);
            Assert.Equal(GetBooksData().Result.Count(), b.Count());// This is true
            Assert.Equal(GetBooksData().ToString(), b.ToString()); // These are not
            Assert.True(bookList.Equals(b));//                        These are not
        }



        private async Task<IEnumerable<BookDTO>> GetBooksData()
        {
            IEnumerable<BookDTO> booksData = new List<BookDTO>
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