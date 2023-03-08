using Microsoft.EntityFrameworkCore;
namespace libraryAPI.Models
{
    public class BookContext : DbContext
    {

        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; } = null!;
    }
}
