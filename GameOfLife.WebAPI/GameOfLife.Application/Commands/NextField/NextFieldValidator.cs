using FluentValidation;

namespace GameOfLife.Application.Commands
{
    public class NextFieldValidator : AbstractValidator<NextFieldCommand>
    {
        public NextFieldValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
        }
    }
}