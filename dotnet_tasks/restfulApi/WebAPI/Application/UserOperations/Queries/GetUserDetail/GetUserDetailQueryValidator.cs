using FluentValidation;

namespace WebAPI.Application.UserOperations.Queries.GetUserDetail;

public class GetUserDetailQueryValidator : AbstractValidator<GetUserDetailQuery>
{
    public GetUserDetailQueryValidator()
    {
        RuleFor(query => query.UserId).GreaterThan(0);
    }
}
