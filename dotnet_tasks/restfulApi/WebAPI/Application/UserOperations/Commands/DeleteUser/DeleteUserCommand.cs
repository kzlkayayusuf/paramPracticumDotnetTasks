using System;
using System.Linq;
using WebAPI.DBOperations;
using static WebAPI.Application.UserOperations.Queries.GetUserDetail.GetUserDetailQuery;

namespace WebAPI.Application.UserOperations.Commands.DeleteUser;

public class DeleteUserCommand
{
    public int UserId { get; set; }
    private readonly ICartoonDbContext context;
    private readonly IMapper mapper;

    public DeleteUserCommand(ICartoonDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public ServiceResponse<UserDetailViewModel> Handle()
    {
        var user = context.Users.Where(u => u.Id == UserId).FirstOrDefault();

        if (user is null)
            throw new InvalidOperationException($"The user with Id '{UserId}' to be deleted was not found");

        UserDetailViewModel vm = mapper.Map<UserDetailViewModel>(user);

        context.Users.Remove(user);
        context.SaveChanges();

        return new ServiceResponse<UserDetailViewModel>(vm);
    }
}

