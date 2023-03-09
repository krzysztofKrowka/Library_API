using LibraryCore.Domain;
using LibraryInfrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInfrastructure.Commands
{
    public class Command
    {
        public static BookDTO BookToDTO(Book book)
        {
            return new BookDTO
            {
                Title = book.Title,
                Description = book.Description,
                Author = book.Author,
                Category = book.Category,
                PublicationDate = book.PublicationDate,
                Cost = book.Cost
            };
        }
    }
}
