using Moq;
using Library.Services.Interfaces;
using Library.Services.Models;
using Library.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Library.Repositories.Models;

namespace Library.Services.Test
{
  /*  public class AuthorTest
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
            Assert.NotNull(authorsFromController);                                          // This is not true
            Assert.Equal(authorsFromMethod.Count(), authorsFromController.Count());        // This is not true
            Assert.True(authorsFromMethod.Equals(authorsFromController));                 // This is not true
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
            Assert.NotNull(authorFromController);                                       
            Assert.True(authorFromMethod.Equals(authorFromController));               
        }

        [Fact]
        public async void AddAuthor()
        {
            //arrange
            var authorList = GetAuthorsData();
            var authorsFromMethod = authorList.Result;
            
            var authorDTO = new AuthorDTO
            {
                FirstName = authorsFromMethod.ElementAt(2).FirstName,
                LastName = authorsFromMethod.ElementAt(2).LastName,
                BirthDate = authorsFromMethod.ElementAt(2).BirthDate,
                AuthorID = authorsFromMethod.ElementAt(2).AuthorID
            };
            
            authorService.Setup(x => x.CreateAuthor(authorDTO)).
                          Returns(Task.FromResult(authorsFromMethod.ElementAt(2)));
            var authorsController = new AuthorsController(authorService.Object);

            //act
            var authorResult = await authorsController.PostAuthor(authorDTO.FirstName,authorDTO.LastName,authorDTO.BirthDate);
            var authorFromController = (authorResult.Result as CreatedResult).Value as Author;

            //assert
            Assert.NotNull(authorFromController);
            Assert.Equal(authorsFromMethod.ElementAt(2).AuthorID, authorFromController.ID);
            Assert.True(authorsFromMethod.ElementAt(2).AuthorID == authorFromController.ID);
        }
        
        private async Task<IEnumerable<AuthorDTO>> GetAuthorsData()
        {
            IEnumerable<AuthorDTO> authorsData = new List<AuthorDTO>
        {

            new AuthorDTO
            {
                FirstName = "Jan",
                LastName = "Nowak",
                BirthDate = DateTime.Parse("2020-02-20"),
                AuthorID = Guid.Parse("78111edf-a63e-4402-a1b4-6a03afdcb4eb")
            },
             new AuthorDTO
            {
                FirstName = "Andrzej",
                LastName = "Kowalski",
                BirthDate = DateTime.Parse("2020-02-20"),
                AuthorID = Guid.Parse("9d3e2274-2ba6-40d6-b173-bd25f301ca1e")
            },
             new AuthorDTO
            {
                FirstName = "Jan",
                LastName = "Tolkien",
                BirthDate = DateTime.Parse("2020-02-20"),
                AuthorID = Guid.Parse("70b78843-c95e-4084-aad5-5af356d645b4")
            }
        };
            return authorsData;
        }
        private async Task<AuthorDTO> GetAuthorData()
        {
            var authorData = new AuthorDTO
            {
                FirstName = "Andrzej",
                LastName = "Kowalski",
                BirthDate = DateTime.Parse("2020-02-20"),
                AuthorID = Guid.Parse("9d3e2274-2ba6-40d6-b173-bd25f301ca1e")
            };
            return authorData;
        }
    }*/
}