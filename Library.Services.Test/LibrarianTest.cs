using Moq;
using Library.Services.Interfaces;
using Library.Services.Models;
using Library.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Library.Repositories.Models;
using Library.Services.Services;

namespace Library.Services.Test
{
    public class LibrarianTest
    {
        private readonly Mock<ILibrarianService> librarianService;
        public LibrarianTest()
        {
            librarianService = new Mock<ILibrarianService>();
        }

        [Fact]
        public async void GetLibrarianList()
        {
            //arrange
            var librarianList = GetLibrariansData();
            librarianService.Setup(x => x.ListLibrarians()).Returns(librarianList);
            var librariansController = new LibrarianController(librarianService.Object);
            var librariansFromMethod = librarianList.Result;

            //act
            var librarianResult = await librariansController.GetLibrarians();
            var librariansFromController = (librarianResult.Result as OkObjectResult).Value as IEnumerable<Librarian>;

            //assert
            Assert.NotNull(librariansFromController);                                        // This is true
            Assert.Equal(librariansFromMethod.Count(), librariansFromController.Count());        // This is true
            Assert.Equal(librariansFromMethod.ToString(), librariansFromController.ToString()); // This is true
            Assert.True(librariansFromMethod.Equals(librariansFromController));                // This is true
        }

        [Fact]
        public async void GetLibrarianByTitle()
        {
            //arrange
            var librarian = GetLibrarianData();
            var librarianFromMethod = librarian.Result;
            librarianService.Setup(x => x.ListLibrarian(
                                Guid.Parse("70b78843-c95e-4084-aad5-5af356d645b4"))).Returns(librarian);
            var librariansController = new LibrarianController(librarianService.Object);

            //act
            var librariansResult = await librariansController.
                                GetLibrarian(Guid.Parse("70b78843-c95e-4084-aad5-5af356d645b4"));
            var librarianFromController = (librariansResult.Result as OkObjectResult).Value as Librarian;

            //assert
            Assert.NotNull(librarianFromController);                                       // This is true
            Assert.Equal(librarianFromMethod.ToString(), librarianFromController.ToString()); // This is true
            Assert.True(librarianFromMethod.Equals(librarianFromController));                // This is true
        }

        [Fact]
        public async void AddLibrarian()
        {
            //arrange
            var librarianList = GetLibrariansData();
            var librariansFromMethod = librarianList.Result;
            
            var librarianDTO = new LibrarianDTO
            {
                FirstName = librariansFromMethod.ElementAt(2).FirstName,
                LastName = librariansFromMethod.ElementAt(2).LastName
            };
            
            librarianService.Setup(x => x.CreateLibrarian(librarianDTO)).
                          Returns(Task.FromResult(librariansFromMethod.ElementAt(2)));
            var librarianController = new LibrarianController(librarianService.Object);

            //act
            var librarianResult = await librarianController.PostLibrarian(librarianDTO);
            var librarianFromController = (librarianResult.Result as CreatedResult).Value as Librarian;

            //assert
            Assert.NotNull(librarianFromController);
            Assert.Equal(librariansFromMethod.ElementAt(2).LibrarianID, librarianFromController.LibrarianID);
            Assert.True(librariansFromMethod.ElementAt(2).LibrarianID == librarianFromController.LibrarianID);
        }

        private async Task<IEnumerable<Librarian>> GetLibrariansData()
        {
            IEnumerable<Librarian> librarianData = new List<Librarian>
        {

            new Librarian
            {
                FirstName = "Jan",
                LastName = "Nowak",
                LibrarianID = Guid.Parse("78111edf-a63e-4402-a1b4-6a03afdcb4eb")
            },
             new Librarian
            {
                FirstName = "Andrzej",
                LastName = "Kowalski",
                LibrarianID = Guid.Parse("9d3e2274-2ba6-40d6-b173-bd25f301ca1e")
            },
             new Librarian
            {
                FirstName = "Jan",
                LastName = "Tolkien",
                LibrarianID = Guid.Parse("70b78843-c95e-4084-aad5-5af356d645b4")
            }
        };
            return librarianData;
        }
        private async Task<Librarian> GetLibrarianData()
        {
            var librarianData = new Librarian
            {
                FirstName = "Andrzej",
                LastName = "Kowalski",
                LibrarianID = Guid.Parse("9d3e2274-2ba6-40d6-b173-bd25f301ca1e")
            };
            return librarianData;
        }
    }
}