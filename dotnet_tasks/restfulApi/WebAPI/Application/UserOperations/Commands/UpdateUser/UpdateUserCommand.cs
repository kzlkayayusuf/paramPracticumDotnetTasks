using System;
using System.Linq;
using WebAPI.DBOperations;
using static WebAPI.Application.UserOperations.Queries.GetUserDetail.GetUserDetailQuery;

namespace WebAPI.Application.UserOperations.Commands.UpdateUser;

public class UpdateUserCommand
{
    public int UserId { get; set; }
    public UpdateUserModel Model { get; set; }
    private readonly ICartoonDbContext context;
    private readonly IMapper mapper;

    public UpdateUserCommand(ICartoonDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public ServiceResponse<UserDetailViewModel> Handle()
    {
        var user = context.Users.Where(u => u.Id == UserId).SingleOrDefault();
        if (user is null)
            throw new InvalidOperationException($"The user with Id '{UserId}' to be updated was not found");


        user.Name = Model.Name != default ? Model.Name : user.Name;
        user.Surname = Model.Surname != default ? Model.Surname : user.Surname;
        user.Email = Model.Email != default ? Model.Email : user.Email;
        user.Password = Model.Password != default ? Model.Password : user.Password;

        context.SaveChanges();

        UserDetailViewModel vm = mapper.Map<UserDetailViewModel>(user);
        return new ServiceResponse<UserDetailViewModel>(vm);
    }

    public class UpdateUserModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
