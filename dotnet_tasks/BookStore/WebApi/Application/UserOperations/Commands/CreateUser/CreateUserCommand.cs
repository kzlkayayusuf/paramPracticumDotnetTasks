using System;
using System.Linq;
using AutoMapper;

namespace WebApi.Application.UserOperations.Commands.CreateUser;

public class CreateUserCommand
{
    public CreateUserModel Model { get; set; }
    private readonly IBookStoreDbContext context;
    private readonly IMapper mapper;

    public CreateUserCommand(IBookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var user = context.Users.SingleOrDefault(u => u.Email == Model.Email);
        if (user is not null)
            throw new InvalidOperationException("User already exists!");

        user = mapper.Map<User>(Model);
        context.Users.Add(user);
        context.SaveChanges();
    }

    public class CreateUserModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
