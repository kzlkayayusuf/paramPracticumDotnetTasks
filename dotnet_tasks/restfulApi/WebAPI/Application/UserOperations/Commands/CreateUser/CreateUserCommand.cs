using System;
using System.Linq;
using WebAPI.DBOperations;
using static WebAPI.Application.UserOperations.Queries.GetUserDetail.GetUserDetailQuery;

namespace WebAPI.Application.UserOperations.Commands.CreateUser;

public class CreateUserCommand
{
    public CreateUserModel Model { get; set; }
    private readonly ICartoonDbContext context;
    private readonly IMapper mapper;

    public CreateUserCommand(ICartoonDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public ServiceResponse<UserDetailViewModel> Handle()
    {
        var user = context.Users.SingleOrDefault(u => u.Email == Model.Email);
        if (user is not null)
            throw new InvalidOperationException("User already exists!");

        user = mapper.Map<User>(Model);
        context.Users.Add(user);
        context.SaveChanges();

        UserDetailViewModel vm = mapper.Map<UserDetailViewModel>(user);
        return new ServiceResponse<UserDetailViewModel>(vm);
    }

    public class CreateUserModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
