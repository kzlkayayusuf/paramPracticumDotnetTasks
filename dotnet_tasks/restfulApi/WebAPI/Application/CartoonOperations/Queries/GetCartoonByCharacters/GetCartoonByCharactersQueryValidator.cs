using FluentValidation;

namespace WebAPI.Application.CartoonOperations.Queries.GetCartoonByCharacters;

public class GetCartoonByCharactersQueryValidator : AbstractValidator<GetCartoonByCharactersQuery>
{
    public GetCartoonByCharactersQueryValidator()
    {
        RuleFor(query => query);
    }
}
