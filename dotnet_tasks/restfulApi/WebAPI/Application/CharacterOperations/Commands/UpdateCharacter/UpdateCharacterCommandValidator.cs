using FluentValidation;

namespace WebAPI.Application.CharacterOperations.Commands.UpdateCharacter;

public class UpdateCharacterCommandValidator : AbstractValidator<UpdateCharacterCommand>
{
    public UpdateCharacterCommandValidator()
    {
        RuleFor(command => command);
    }
}
