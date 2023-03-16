using Microsoft.EntityFrameworkCore;

namespace Library.Repositories.Models
{
    public partial class BookContext : DbContext 
    { 
       public BookContext() { 
        }
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<BookAuthors> BookAuthors { get; set; }
        public virtual DbSet<Librarian> Librarians { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=library;Trusted_Connection=True;TrustServerCertificate=True;",b => b.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));
            base.OnConfiguring(optionsBuilder);
            
        }


    }
}
