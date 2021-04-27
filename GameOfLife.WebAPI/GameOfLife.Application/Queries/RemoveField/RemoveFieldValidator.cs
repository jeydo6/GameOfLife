using FluentValidation;

namespace GameOfLife.Application.Queries
{
	public class RemoveFieldValidator : AbstractValidator<RemoveFieldQuery>
	{
		public RemoveFieldValidator()
		{
			RuleFor(q => q.Id)
				.NotEmpty();
		}
	}
}
