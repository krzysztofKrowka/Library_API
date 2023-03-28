using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories.Models
{
    public partial class LibraryContext : DbContext, ILibraryContext 
    { 
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) {  }
        
        
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthors> BookAuthors { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
        public List<User> Users { get; } = new()
        {
            new User(){ Username="librarian",Password="librarian",Role="Librarian"},
            new User(){ Username="assistant",Password="assistant",Role="Assistant"},
            new User(){ Username="reader",Password="reader",Role="Reader"},
        };

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=library;Trusted_Connection=True;TrustServerCertificate=True;",b => b.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
