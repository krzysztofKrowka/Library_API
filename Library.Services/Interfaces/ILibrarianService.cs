using Library.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Services.Models;
namespace Library.Services.Interfaces
{
    public interface ILibrarianService
    {
        Task<Librarian> ListLibrarian(Guid librarianID);
        
        Task<IEnumerable<Librarian>> ListLibrarians();
        
        Task<Librarian> CreateLibrarian(LibrarianDTO librarian);
        
        Task<bool> DeleteLibrarian(Guid librarianID);

    }
}
