using FluentValidation;

namespace WebAPI.Application.CharacterOperations.Commands.CreateCharacter;

public class CreateCharacterCommandValidator : AbstractValidator<CreateCharacterCommand>
{
    public CreateCharacterCommandValidator()
    {
        RuleFor(command => command.Model.CartoonId).GreaterThan(0);
        RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(3);
    }
}
