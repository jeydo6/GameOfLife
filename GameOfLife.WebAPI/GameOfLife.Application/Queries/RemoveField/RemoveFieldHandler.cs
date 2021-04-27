using GameOfLife.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.Application.Queries
{
	public class RemoveFieldHandler : IRequestHandler<RemoveFieldQuery>
	{
		private readonly IFieldsRepository _fields;

		public RemoveFieldHandler(
			IFieldsRepository fields
		)
		{
			_fields = fields;
		}

		public async Task<Unit> Handle(RemoveFieldQuery request, CancellationToken cancellationToken)
		{
			await _fields.Remove(request.Id);

			return Unit.Value;
		}
	}
}
