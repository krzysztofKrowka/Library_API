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
        Librarian ListLibrarian(Guid librarianID);
        IEnumerable<Librarian> ListLibrarians();
        Librarian CreateLibrarian(LibrarianDTO librarian);
        bool DeleteLibrarian(Guid librarianID);

    }
}
