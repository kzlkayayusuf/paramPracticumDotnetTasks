using FluentValidation;

namespace WebAPI.Application.UserOperations.Commands.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(command => command.UserId).GreaterThan(0);
    }
}
