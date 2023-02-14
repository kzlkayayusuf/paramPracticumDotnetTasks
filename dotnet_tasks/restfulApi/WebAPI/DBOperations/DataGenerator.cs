using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.DBOperations;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new CartoonDbContext(serviceProvider.GetRequiredService<DbContextOptions<CartoonDbContext>>()))
        {
            if (context.Cartoons.Any())
                return;

            context.Users.AddRange(
                new User { Name = "user1", Surname = "user", Email = "user1@example.com", Password = "password1" },
                new User { Name = "user2", Surname = "user1", Email = "user2@example.com", Password = "password2" },
                new User { Name = "user3", Surname = "user2", Email = "user3@example.com", Password = "password3" }
            );

            context.Cartoons.AddRange(
                new Cartoon
                {
                    Name = "Tom and Jerry",
                    Genre = Genre.Comedy,
                    ReleaseDate = new DateTime(1940, 2, 10),
                    Topic = "Slapstick comedy",
                },
                new Cartoon
                {
                    Name = "SpongeBob SquarePants",
                    Genre = Genre.Comedy,
                    ReleaseDate = new DateTime(1999, 5, 1),
                    Topic = "Underwater adventures",
                },
                new Cartoon
                {
                    Name = "The Simpsons",
                    Genre = Genre.Comedy,
                    ReleaseDate = new DateTime(1989, 12, 17),
                    Topic = "Family life in a suburban town",
                },
                new Cartoon
                {
                    Name = "Teenage Mutant Ninja Turtles",
                    Genre = Genre.Action,
                    ReleaseDate = new DateTime(1987, 12, 10),
                    Topic = "Crime-fighting mutant turtles",
                },
                new Cartoon
                {
                    Name = "Adventure Time",
                    Genre = Genre.Adventure,
                    ReleaseDate = new DateTime(2010, 4, 5),
                    Topic = "Surreal adventures in a post-apocalyptic world",
                }
            );

            context.CartoonCharacters.AddRange(
                new CartoonCharacter
                {
                    CartoonID = 1,
                    Name = "Tom"
                },
                new CartoonCharacter
                {
                    CartoonID = 1,
                    Name = "Jerry"
                },
                new CartoonCharacter
                {
                    CartoonID = 2,
                    Name = "SpongeBob"
                },
                new CartoonCharacter
                {
                    CartoonID = 2,
                    Name = "Patrick"
                },
                new CartoonCharacter
                {
                    CartoonID = 2,
                    Name = "Sandy"
                },
                new CartoonCharacter
                {
                    CartoonID = 3,
                    Name = "Homer"
                },
                new CartoonCharacter
                {
                    CartoonID = 3,
                    Name = "Marge"
                },
                new CartoonCharacter
                {
                    CartoonID = 3,
                    Name = "Bart"
                },
                 new CartoonCharacter
                 {
                     CartoonID = 3,
                     Name = "Lisa"
                 },
                new CartoonCharacter
                {
                    CartoonID = 3,
                    Name = "Maggie"
                },
                new CartoonCharacter
                {
                    CartoonID = 4,
                    Name = "Leonardo"
                },
                new CartoonCharacter
                {
                    CartoonID = 4,
                    Name = "Michelangelo"
                },
                new CartoonCharacter
                {
                    CartoonID = 4,
                    Name = "Donatello"
                },
                 new CartoonCharacter
                 {
                     CartoonID = 4,
                     Name = "Raphael"
                 },
                 new CartoonCharacter
                 {
                     CartoonID = 5,
                     Name = "Finn"
                 },
                 new CartoonCharacter
                 {
                     CartoonID = 5,
                     Name = "Jake"
                 },
                new CartoonCharacter
                {
                    CartoonID = 5,
                    Name = "Princess Bubblegum"
                }
            );

            context.SaveChanges();
        }
    }
}
