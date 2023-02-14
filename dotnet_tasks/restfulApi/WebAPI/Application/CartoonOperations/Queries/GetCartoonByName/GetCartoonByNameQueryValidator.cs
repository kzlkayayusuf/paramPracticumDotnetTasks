using FluentValidation;

namespace WebAPI.Application.CartoonOperations.Queries.GetCartoonByName;

public class GetCartoonByNameQueryValidator : AbstractValidator<GetCartoonByNameQuery>
{
    public GetCartoonByNameQueryValidator()
    {
        RuleFor(query => query.CartoonName).NotEmpty().MinimumLength(3);
    }
}
