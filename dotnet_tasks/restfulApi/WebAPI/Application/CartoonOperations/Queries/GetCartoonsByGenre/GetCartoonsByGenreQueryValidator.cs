using FluentValidation;

namespace WebAPI.Application.CartoonOperations.Queries.GetCartoonsByGenre;

public class GetCartoonsByGenreQueryValidator : AbstractValidator<GetCartoonsByGenreQuery>
{
    public GetCartoonsByGenreQueryValidator()
    {
        RuleFor(query => query.CartoonGenre).NotEmpty().WithMessage("Please provide a valid Genre type {Comedy},{Action} or {Adventure}");
    }
}
