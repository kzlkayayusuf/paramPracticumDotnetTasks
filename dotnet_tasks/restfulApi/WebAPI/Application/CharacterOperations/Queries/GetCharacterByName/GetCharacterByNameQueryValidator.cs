using FluentValidation;

namespace WebAPI.Application.CharacterOperations.Queries.GetCharacterByName;

public class GetCharacterByNameQueryValidator : AbstractValidator<GetCharacterByNameQuery>
{
    public GetCharacterByNameQueryValidator()
    {
        RuleFor(query => query.CharacterName).NotEmpty().MinimumLength(3);
    }
}
