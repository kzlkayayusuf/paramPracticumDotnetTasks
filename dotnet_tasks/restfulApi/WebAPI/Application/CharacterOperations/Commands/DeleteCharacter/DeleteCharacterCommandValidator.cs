using FluentValidation;

namespace WebAPI.Application.CharacterOperations.Commands.DeleteCharacter;

public class DeleteCharacterCommandValidator : AbstractValidator<DeleteCharacterCommand>
{
    public DeleteCharacterCommandValidator()
    {
        RuleFor(command => command.CharacterId).GreaterThan(0);
    }
}
