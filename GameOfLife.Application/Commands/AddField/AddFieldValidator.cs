using FluentValidation;

namespace GameOfLife.Application.Commands
{
    public class AddFieldValidator : AbstractValidator<AddFieldCommand>
    {
        public AddFieldValidator()
        {
            RuleFor(c => c.Size)
                .NotEmpty();

            RuleFor(c => c.Density)
                .NotEmpty();
        }
    }
}