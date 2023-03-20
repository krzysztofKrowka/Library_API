using Library.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.Interfaces
{
    public interface ILibrarianRepository
    {
        Task<Librarian> ListLibrarian(Guid librarianID);
        Task<IEnumerable<Librarian>> ListLibrarians();
        Task<Librarian> CreateLibrarian(Librarian librarian);
        Task<bool> DeleteLibrarian(Guid librarianID);
    }
}
