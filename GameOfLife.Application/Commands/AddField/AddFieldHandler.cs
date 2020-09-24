using GameOfLife.Domain.Entities;
using GameOfLife.Domain.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.Application.Commands
{
    public class AddFieldHandler : IRequestHandler<AddFieldCommand, Guid>
    {
        private readonly IFieldsRepository _fields;

        public AddFieldHandler(
            IFieldsRepository fields
        )
        {
            _fields = fields;
        }

        public async Task<Guid> Handle(AddFieldCommand request, CancellationToken cancellationToken)
        {
            return await _fields.Add(
                new Field(request.Size, Life)
            );
        }

        private void Life(Field field)
        {
            for (UInt16 i = 0; i < field.Size; i++)
            {
                for (UInt16 j = 0; j < field.Size; j++)
                {
                    field.Values[i, j] = !field.Values[i, j];
                }
            }
        }
    }
}
