using Microsoft.EntityFrameworkCore;

namespace WebAPI.DBOperations;

public class CartoonDbContext : DbContext, ICartoonDbContext
{
    public CartoonDbContext(DbContextOptions<CartoonDbContext> options) : base(options)
    {
    }

    public DbSet<Cartoon> Cartoons { get; set; }
    public DbSet<CartoonCharacter> CartoonCharacters { get; set; }
    public DbSet<User> Users { get; set; }

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }

}
