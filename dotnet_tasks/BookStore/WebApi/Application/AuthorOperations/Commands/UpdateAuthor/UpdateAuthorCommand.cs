using System;
using System.Linq;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommand
{
    private readonly IBookStoreDbContext context;

    public int AuthorId { get; set; }
    public UpdateAuthorModel Model { get; set; }
    public UpdateAuthorCommand(IBookStoreDbContext context)
    {
        this.context = context;
    }

    public void Handle()
    {
        var author = context.Authors.SingleOrDefault(x => x.Id == AuthorId);
        if (author is null)
            throw new InvalidOperationException("No author found to be updated");

        author.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? author.Name : Model.Name;
        author.Surname = string.IsNullOrEmpty(Model.Surname.Trim()) ? author.Surname : Model.Surname;
        author.Birthday = Model.Birthday != default ? Model.Birthday : author.Birthday;

        context.SaveChanges();
    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
    }
}
