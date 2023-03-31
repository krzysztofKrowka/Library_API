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
            
            authorService.Setup(x => x.ListAuthors(1, 1)).Returns(authorList);
            
            var authorsController = new AuthorsController(authorService.Object);
            var authorsFromMethod = authorList.Result;

            //act
            var authorResult = await authorsController.GetAuthors(1,1);
            var authorsFromController = (authorResult.Result as OkObjectResult).Value as IEnumerable<AuthorDTO>;

            
            //assert
            Assert.NotNull(authorsFromController);                                          
            Assert.Equal(authorsFromMethod.Count(), authorsFromController.Count());       
            Assert.True(authorsFromMethod.Equals(authorsFromController));                 
        
        }

        [Fact]
        public async void GetAuthorByID()
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
            
            var authorFromController = (authorsResult.Result as OkObjectResult).Value as AuthorDTO;

            
            //assert
            Assert.NotNull(authorFromController);                                       
            Assert.True(authorFromMethod.Equals(authorFromController));               
        
        }

        [Fact]
        public async void AddAuthor()
        {
            //arrange
            var authorsFromMethod =await GetAuthorsData();
            var author = authorsFromMethod.ElementAt(0);

            var authorDTO = new AuthorDTO
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                BirthDate = author.BirthDate
            };
            
            authorService.Setup(x => x.CreateAuthor(authorDTO.FirstName, authorDTO.LastName, authorDTO.BirthDate)).
                          Returns(Task.FromResult(author));
            
            var authorsController = new AuthorsController(authorService.Object);

            //act
            var authorResult =await authorsController.PostAuthor(authorDTO.FirstName,authorDTO.LastName,authorDTO.BirthDate);
            var authorFromController = (authorResult.Result as CreatedResult).Value as AuthorDTO;

            
            //assert
            Assert.NotNull(authorFromController);
            Assert.Equal(author.AuthorID, authorFromController.AuthorID);
            Assert.True(author.AuthorID == authorFromController.AuthorID);
        
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
                    AuthorID = Guid.Parse("78111edf-a63e-4402-a1b4-6a03afdcb4eb"),
                    Books = new List<BookDTO>()
                },
                 
                new AuthorDTO
                {
                    FirstName = "Andrzej",
                    LastName = "Kowalski",
                    BirthDate = DateTime.Parse("2020-02-20"),
                    AuthorID = Guid.Parse("9d3e2274-2ba6-40d6-b173-bd25f301ca1e"),
                    Books = new List<BookDTO>()
                },
                 
                new AuthorDTO
                {
                    FirstName = "Jan",
                    LastName = "Tolkien",
                    BirthDate = DateTime.Parse("2020-02-20"),
                    AuthorID = Guid.Parse("70b78843-c95e-4084-aad5-5af356d645b4"),
                    Books = new List<BookDTO>()
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
                AuthorID = Guid.Parse("9d3e2274-2ba6-40d6-b173-bd25f301ca1e"),
                Books = new List<BookDTO>()
            };

            return authorData;
        
        }
    }
}