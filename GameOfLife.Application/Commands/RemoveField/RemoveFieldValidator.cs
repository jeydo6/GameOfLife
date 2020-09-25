using FluentValidation;

namespace GameOfLife.Application.Commands
{
    public class RemoveFieldValidator : AbstractValidator<RemoveFieldCommand>
    {
        public RemoveFieldValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty();
        }
    }
}
