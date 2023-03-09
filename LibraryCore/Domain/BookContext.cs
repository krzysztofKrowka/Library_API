using Microsoft.EntityFrameworkCore;
namespace LibraryCore.Domain;

public class BookContext : DbContext
{

    public BookContext(DbContextOptions<BookContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; } = null!;
}
