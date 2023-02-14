using FluentValidation;

namespace WebAPI.Application.CartoonOperations.Queries.GetCartoonDetail;

public class GetCartoonDetailQueryValidator : AbstractValidator<GetCartoonDetailQuery>
{
    public GetCartoonDetailQueryValidator()
    {
        RuleFor(query => query.CartoonId).GreaterThan(0);
    }
}
