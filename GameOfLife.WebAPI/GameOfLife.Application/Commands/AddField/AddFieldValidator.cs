using FluentValidation;
using System;

namespace GameOfLife.Application.Commands
{
    public class AddFieldValidator : AbstractValidator<AddFieldCommand>
    {
        public AddFieldValidator()
        {
            RuleFor(c => c.Size)
                .NotEmpty()
                .GreaterThan((UInt16)2);

            RuleFor(c => c.Density)
                .NotEmpty();

            RuleFor(c => c.BehaviorEnum)
                .IsInEnum();
        }
    }
}