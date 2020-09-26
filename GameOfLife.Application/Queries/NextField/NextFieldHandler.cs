using AutoMapper;
using GameOfLife.Application.Dto;
using GameOfLife.Domain.Entities;
using GameOfLife.Domain.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.Application.Queries
{
    public class NextFieldHandler : IRequestHandler<NextFieldQuery, FieldDto>
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


        public async Task<FieldDto> Handle(NextFieldQuery request, CancellationToken cancellationToken)
        {
            Field field = await _fields.Get(request.Id);

            return _mapper.Map<FieldDto>(field?.Next());
        }
    }
}
