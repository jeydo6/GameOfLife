using FluentValidation;

namespace GameOfLife.Application.Queries
{
    public class GetFieldValidator : AbstractValidator<GetFieldQuery>
    {
        public GetFieldValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty();
        }
    }
}
