using Microsoft.EntityFrameworkCore;

namespace WebAPI.DBOperations;

public interface ICartoonDbContext
{
    DbSet<Cartoon> Cartoons { get; set; }
    DbSet<CartoonCharacter> CartoonCharacters { get; set; }
    DbSet<User> Users { get; set; }

    int SaveChanges();
}
