using FluentValidation;

namespace WebAPI.Application.CartoonOperations.Commands.DeleteCartoon;

public class DeleteCartoonCommandValidator : AbstractValidator<DeleteCartoonCommand>
{
    public DeleteCartoonCommandValidator()
    {
        RuleFor(command => command.CartoonId).GreaterThan(0);
    }
}
