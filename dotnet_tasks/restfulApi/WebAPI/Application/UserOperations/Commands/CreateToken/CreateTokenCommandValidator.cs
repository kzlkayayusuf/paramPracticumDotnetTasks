using FluentValidation;

namespace WebAPI.Application.UserOperations.Commands.CreateToken;

public class CreateTokenCommandValidator : AbstractValidator<CreateTokenCommand>
{
    public CreateTokenCommandValidator()
    {
        RuleFor(command => command.Model.Email).NotEmpty().EmailAddress();
        RuleFor(command => command.Model.Password).NotEmpty().Length(6, 100);
    }
}
