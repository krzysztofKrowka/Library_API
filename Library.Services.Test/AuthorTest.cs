using Moq;
using Library.Services.Interfaces;
using Library.Services.Models;
using Library.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Library.Repositories.Models;

namespace Library.Services.Test
{
    public class AuthorTest
    {
        private readonly Mock<IAuthorService> authorService;
        public AuthorTest()
        {
            authorService = new Mock<IAuthorService>();
        }

        [Fact]
        public async void GetAuthorList()
        {
            //arrange
            var authorList = GetAuthorsData();
            authorService.Setup(x => x.ListAuthors()).Returns(authorList);
            var authorsController = new AuthorsController(authorService.Object);
            var authorsFromMethod = authorList.Result;

            //act
            var authorResult = await authorsController.GetAuthors();
            var authorsFromController = (authorResult.Result as OkObjectResult).Value as IEnumerable<Author>;

            //assert
            Assert.NotNull(authorsFromController);                                        // This is true
            Assert.Equal(authorsFromMethod.Count(), authorsFromController.Count());        // This is true
            Assert.Equal(authorsFromMethod.ToString(), authorsFromController.ToString()); // This is true
            Assert.True(authorsFromMethod.Equals(authorsFromController));                // This is true
        }

        [Fact]
        public async void GetAuthorByTitle()
        {
            //arrange
            var author = GetAuthorData();
            var authorFromMethod = author.Result;
            authorService.Setup(x => x.ListAuthor(
                                Guid.Parse("70b78843-c95e-4084-aad5-5af356d645b4"))).Returns(author);
            var authorsController = new AuthorsController(authorService.Object);

            //act
            var authorsResult = await authorsController.
                                GetAuthor(Guid.Parse("70b78843-c95e-4084-aad5-5af356d645b4"));
            var authorFromController = (authorsResult.Result as OkObjectResult).Value as Author;

            //assert
            Assert.NotNull(authorFromController);                                       // This is true
            Assert.Equal(authorFromMethod.ToString(), authorFromController.ToString()); // This is true
            Assert.True(authorFromMethod.Equals(authorFromController));                // This is true
        }

        //I don't know why this does not work
        //It's the exact same as the one in LibrarianTest
        [Fact]
        public async void AddAuthor()
        {
            //arrange
            var authorList = GetAuthorsData();
            var authorsFromMethod = authorList.Result;
            
            var authorDTO = new AuthorDTO
            {
                FirstName = authorsFromMethod.ElementAt(2).FirstName,
                LastName = authorsFromMethod.ElementAt(2).LastName
            };
            
            authorService.Setup(x => x.CreateAuthor(authorDTO)).
                          Returns(Task.FromResult(authorsFromMethod.ElementAt(2)));
            var authorsController = new AuthorsController(authorService.Object);

            //act
            var authorResult = await authorsController.PostAuthor(authorDTO);
            var authorFromController = (authorResult.Result as CreatedResult).Value as Author;

            //assert
            Assert.NotNull(authorFromController);
            Assert.Equal(authorsFromMethod.ElementAt(2).AuthorID, authorFromController.AuthorID);
            Assert.True(authorsFromMethod.ElementAt(2).AuthorID == authorFromController.AuthorID);
        }
        
        private async Task<IEnumerable<Author>> GetAuthorsData()
        {
            IEnumerable<Author> authorsData = new List<Author>
        {

            new Author
            {
                FirstName = "Jan",
                LastName = "Nowak",
                AuthorID = Guid.Parse("78111edf-a63e-4402-a1b4-6a03afdcb4eb")
            },
             new Author
            {
                FirstName = "Andrzej",
                LastName = "Kowalski",
                AuthorID = Guid.Parse("9d3e2274-2ba6-40d6-b173-bd25f301ca1e")
            },
             new Author
            {
                FirstName = "Jan",
                LastName = "Tolkien",
                AuthorID = Guid.Parse("70b78843-c95e-4084-aad5-5af356d645b4")
            }
        };
            return authorsData;
        }
        private async Task<Author> GetAuthorData()
        {
            var authorData = new Author
            {
                FirstName = "Andrzej",
                LastName = "Kowalski",
                AuthorID = Guid.Parse("9d3e2274-2ba6-40d6-b173-bd25f301ca1e")
            };
            return authorData;
        }
    }
}