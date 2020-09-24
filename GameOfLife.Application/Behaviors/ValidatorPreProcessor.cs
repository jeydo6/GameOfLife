using FluentValidation;
using MediatR.Pipeline;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.Application.Behaviors
{
    public class ValidatorPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly IValidator<TRequest> _validator;

        public ValidatorPreProcessor(
            IValidator<TRequest> validator
        )
        {
            _validator = validator;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request);

            var failures = result.Errors
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                throw new ValidationException("Validation exception", failures);
            }
        }
    }
}
