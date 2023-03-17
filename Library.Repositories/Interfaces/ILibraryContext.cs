using Library.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.Interfaces
{
    public interface ILibraryContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthors> BookAuthors { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
        public List<User> Users { get; }

        int SaveChanges();
    }
}
