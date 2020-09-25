using GameOfLife.Domain.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.Application.Commands
{
    public class RemoveFieldHandler : IRequestHandler<RemoveFieldCommand>
    {
        private readonly IFieldsRepository _fields;

        public RemoveFieldHandler(
            IFieldsRepository fields
        )
        {
            _fields = fields;
        }

        public async Task<Unit> Handle(RemoveFieldCommand request, CancellationToken cancellationToken)
        {
            await _fields.Remove(request.Id);

            return Unit.Value;
        }
    }
}
