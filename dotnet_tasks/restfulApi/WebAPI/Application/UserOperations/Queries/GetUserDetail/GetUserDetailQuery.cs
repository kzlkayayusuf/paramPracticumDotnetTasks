using System;
using System.Linq;
using WebAPI.DBOperations;

namespace WebAPI.Application.UserOperations.Queries.GetUserDetail;

public class GetUserDetailQuery
{
    public int UserId { get; set; }
    private readonly ICartoonDbContext context;
    private readonly IMapper mapper;

    public GetUserDetailQuery(ICartoonDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public ServiceResponse<UserDetailViewModel> Handle()
    {
        var user = context.Users.Where(u => u.Id == UserId).SingleOrDefault();
        if (user is null)
            throw new Exception($"User with Id '{UserId}' not found.");
        UserDetailViewModel vm = mapper.Map<UserDetailViewModel>(user);

        return new ServiceResponse<UserDetailViewModel>(vm);
    }

    public class UserDetailViewModel
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
