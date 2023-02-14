using FluentValidation;

namespace WebAPI.Application.UserOperations.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(command => command.Model.Name).NotEmpty().Length(3, 50);
        RuleFor(command => command.Model.Surname).NotEmpty().Length(3, 50);
        RuleFor(command => command.Model.Email).NotEmpty().EmailAddress();
        RuleFor(command => command.Model.Password).NotEmpty().Length(6, 100);
    }
}
