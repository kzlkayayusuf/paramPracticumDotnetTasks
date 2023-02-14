using System;
using FluentValidation;

namespace WebAPI.Application.CartoonOperations.Commands.UpdateCartoon;

public class UpdateCartoonCommandValidator : AbstractValidator<UpdateCartoonCommand>
{
    public UpdateCartoonCommandValidator()
    {
        RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(10);
        RuleFor(command => command.Model.ReleaseDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
        RuleFor(command => command.Model.Topic).NotEmpty().MinimumLength(10);
        RuleFor(command => command.Model.Genre).NotEmpty().WithMessage("Please provide a valid Genre type {Comedy},{Action} or {Adventure}");
    }
}
