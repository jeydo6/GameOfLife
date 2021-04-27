using AutoMapper;
using GameOfLife.Application.Dto;
using GameOfLife.Domain.Entities;
using GameOfLife.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.Application.Queries
{
	public class GetFieldHandler : IRequestHandler<GetFieldQuery, FieldDto>
	{
		private readonly IFieldsRepository _fields;
		private readonly IMapper _mapper;

		public GetFieldHandler(
			IFieldsRepository fields,
			IMapper mapper
		)
		{
			_fields = fields;
			_mapper = mapper;
		}

		public async Task<FieldDto> Handle(GetFieldQuery request, CancellationToken cancellationToken)
		{
			Field field = await _fields.Get(request.Id);

			return _mapper.Map<FieldDto>(field);
		}
	}
}
