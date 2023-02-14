using System.Collections.Generic;
using WebAPI.Application.UserOperations.Commands.CreateToken;
using WebAPI.Application.UserOperations.Commands.CreateUser;
using WebAPI.Application.UserOperations.Commands.UpdateUser;
using WebAPI.Application.UserOperations.Queries.GetUserDetail;
using WebAPI.Application.UserOperations.Queries.GetUsers;
using WebAPI.TokenOperations.Models;

namespace WebAPI.Services.UserService;

public interface IUserService
{
    ServiceResponse<List<GetUsersQuery.UsersViewModel>> GetAllUsers();
    ServiceResponse<GetUserDetailQuery.UserDetailViewModel> GetUserById(int id);
    ServiceResponse<GetUserDetailQuery.UserDetailViewModel> CreateUser(CreateUserCommand.CreateUserModel newUser);
    ServiceResponse<Token> CreateToken(CreateTokenCommand.CreateTokenModel newToken);
    ServiceResponse<Token> RefreshToken(string token);
    ServiceResponse<GetUserDetailQuery.UserDetailViewModel> DeleteUser(int id);
    ServiceResponse<GetUserDetailQuery.UserDetailViewModel> UpdateUser(int id, UpdateUserCommand.UpdateUserModel user);
}
