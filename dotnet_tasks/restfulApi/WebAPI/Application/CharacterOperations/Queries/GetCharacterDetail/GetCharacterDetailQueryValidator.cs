using FluentValidation;

namespace WebAPI.Application.CharacterOperations.Queries.GetCharacterDetail;

public class GetCharacterDetailQueryValidator : AbstractValidator<GetCharacterDetailQuery>
{
    public GetCharacterDetailQueryValidator()
    {
        RuleFor(query => query.CharacterId).GreaterThan(0);
    }
}
