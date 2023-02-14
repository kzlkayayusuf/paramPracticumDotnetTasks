using System.Collections.Generic;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using WebAPI.Application.UserOperations.Commands.CreateToken;
using WebAPI.Application.UserOperations.Commands.CreateUser;
using WebAPI.Application.UserOperations.Commands.DeleteUser;
using WebAPI.Application.UserOperations.Commands.RefreshToken;
using WebAPI.Application.UserOperations.Commands.UpdateUser;
using WebAPI.Application.UserOperations.Queries.GetUserDetail;
using WebAPI.Application.UserOperations.Queries.GetUsers;
using WebAPI.DBOperations;
using WebAPI.TokenOperations.Models;

namespace WebAPI.Services.UserService;

public class UserService : IUserService
{
    private readonly ICartoonDbContext context;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;

    public UserService(ICartoonDbContext context, IMapper mapper, IConfiguration configuration)
    {
        this.context = context;
        this.mapper = mapper;
        this.configuration = configuration;
    }

    public ServiceResponse<List<GetUsersQuery.UsersViewModel>> GetAllUsers()
    {
        GetUsersQuery query = new(context, mapper);


        return query.Handle();
    }

    public ServiceResponse<GetUserDetailQuery.UserDetailViewModel> GetUserById(int id)
    {
        GetUserDetailQuery query = new(context, mapper);
        query.UserId = id;
        GetUserDetailQueryValidator validator = new();
        validator.ValidateAndThrow(query);

        return query.Handle();
    }

    public ServiceResponse<GetUserDetailQuery.UserDetailViewModel> CreateUser(CreateUserCommand.CreateUserModel newUser)
    {
        CreateUserCommand command = new(context, mapper);
        command.Model = newUser;

        CreateUserCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        return command.Handle();
    }

    public ServiceResponse<Token> CreateToken(CreateTokenCommand.CreateTokenModel newToken)
    {
        CreateTokenCommand command = new(context, mapper, configuration);
        command.Model = newToken;

        CreateTokenCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        return command.Handle();
    }

    public ServiceResponse<Token> RefreshToken(string token)
    {
        RefreshTokenCommand command = new(context, configuration);
        command.RefreshToken = token;

        return command.Handle();
    }

    public ServiceResponse<GetUserDetailQuery.UserDetailViewModel> DeleteUser(int id)
    {
        DeleteUserCommand command = new(context, mapper);
        command.UserId = id;
        DeleteUserCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        return command.Handle();
    }

    public ServiceResponse<GetUserDetailQuery.UserDetailViewModel> UpdateUser(int id, UpdateUserCommand.UpdateUserModel user)
    {
        UpdateUserCommand command = new(context, mapper);
        command.UserId = id;
        command.Model = user;

        UpdateUserCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        return command.Handle();
    }
}
