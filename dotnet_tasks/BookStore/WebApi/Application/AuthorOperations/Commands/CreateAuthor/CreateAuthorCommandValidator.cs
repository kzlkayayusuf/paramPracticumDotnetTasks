using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(3);
        RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(4);
        RuleFor(command => command.Model.Birthday).NotEmpty().LessThan(DateTime.Now.Date);
    }
}
