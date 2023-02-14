using System.Collections.Generic;
using System.Linq;
using WebAPI.DBOperations;

namespace WebAPI.Application.UserOperations.Queries.GetUsers;

public class GetUsersQuery
{
    private readonly ICartoonDbContext context;
    private readonly IMapper mapper;

    public GetUsersQuery(ICartoonDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public ServiceResponse<List<UsersViewModel>> Handle()
    {
        var userList = context.Users.OrderBy(u => u.Id).ToList<User>();
        List<UsersViewModel> vm = mapper.Map<List<UsersViewModel>>(userList);

        return new ServiceResponse<List<UsersViewModel>>(vm);
    }

    public class UsersViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
