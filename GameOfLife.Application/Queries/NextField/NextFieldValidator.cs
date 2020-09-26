using FluentValidation;

namespace GameOfLife.Application.Queries
{
    public class NextFieldValidator : AbstractValidator<NextFieldQuery>
    {
        public NextFieldValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty();
        }
    }
}