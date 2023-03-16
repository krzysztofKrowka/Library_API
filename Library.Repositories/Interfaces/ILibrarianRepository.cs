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
        Librarian ListLibrarian(Guid librarianID);
        IEnumerable<Librarian> ListLibrarians();
        Librarian CreateLibrarian(Librarian librarian);
        bool DeleteLibrarian(Guid librarianID);
    }
}
