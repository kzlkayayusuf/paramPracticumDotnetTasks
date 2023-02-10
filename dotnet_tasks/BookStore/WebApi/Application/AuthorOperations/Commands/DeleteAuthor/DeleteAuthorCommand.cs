using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommand
{
    private readonly IBookStoreDbContext context;

    public int AuthorId { get; set; }
    public DeleteAuthorCommand(IBookStoreDbContext context)
    {
        this.context = context;
    }

    public void Handle()
    {
        var author = context.Authors.Include(a => a.Books).SingleOrDefault(x => x.Id == AuthorId);
        if (author is null)
            throw new InvalidOperationException("The author to be deleted was not found");
        if (author.Books.Any())
            throw new InvalidOperationException("Author have book(s) so you can't delete");

        context.Authors.Remove(author);
        context.SaveChanges();
    }
}
