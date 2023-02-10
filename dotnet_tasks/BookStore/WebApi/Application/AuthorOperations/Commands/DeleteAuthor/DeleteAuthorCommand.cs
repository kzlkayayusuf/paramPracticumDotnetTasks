using System;
using System.Linq;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommand
{
    private readonly BookStoreDbContext context;

    public int AuthorId { get; set; }
    public DeleteAuthorCommand(BookStoreDbContext context)
    {
        this.context = context;
    }

    public void Handle()
    {
        var author = context.Authors.SingleOrDefault(x => x.Id == AuthorId);
        if (author is null)
            throw new InvalidOperationException("The author to be deleted was not found");

        context.Authors.Remove(author);
        context.SaveChanges();
    }
}
