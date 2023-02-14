using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace WebAPI.Application.UserOperations.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(command => command.UserId).GreaterThan(0);
        RuleFor(command => command.Model.Name).NotEmpty().Length(3, 50);
        RuleFor(command => command.Model.Surname).NotEmpty().Length(3, 50);
        RuleFor(command => command.Model.Email).NotEmpty().EmailAddress();
        RuleFor(command => command.Model.Password).NotEmpty().Length(6, 100);
    }
}
