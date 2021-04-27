using AutoMapper;
using GameOfLife.Domain.Entities;
using GameOfLife.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.Application.Commands
{
	public class NextFieldHandler : IRequestHandler<NextFieldCommand>
	{
		private readonly IFieldsRepository _fields;
		private readonly IMapper _mapper;

		public NextFieldHandler(
			IFieldsRepository fields,
			IMapper mapper
		)
		{
			_fields = fields;
			_mapper = mapper;
		}


		public async Task<Unit> Handle(NextFieldCommand request, CancellationToken cancellationToken)
		{
			Field field = await _fields.Get(request.Id);
			field?.Next();

			return Unit.Value;
		}
	}
}
