using Microsoft.EntityFrameworkCore;

namespace WebApi.DBOperations;

public class BookStoreDbContext : DbContext
{
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Entities.Genre> Genres { get; set; }
    public DbSet<Author> Authors { get; set; }

    // Entity isimleri tekil olarak yazılır. Referans olarak DB de oluşturulacak isim çoğul olur.
}
