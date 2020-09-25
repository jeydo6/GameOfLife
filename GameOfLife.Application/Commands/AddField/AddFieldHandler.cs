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
                new Field(request.Size, request.Density, Life)
            );
        }

        private void Life(Field field)
        {
            UInt16 size = field.Size;
            Byte[] values = new Byte[size * size];
            field.Values.CopyTo(values, 0);

            for (UInt16 i = 1; i < size - 1; i++)
            {
                for (UInt16 j = 1; j < size - 1; j++)
                {
                    UInt16 p = (UInt16)(j * size + i);

                    field.Values[p] = GetValue(
                        field.Values[p],
                        GetNeighboursCount(values, size, x: i, y: j)
                    );
                }
            }
        }

        private Byte GetNeighboursCount(Byte[] values, UInt16 size, UInt16 x, UInt16 y)
        {
            Byte count1 = 0;

            for (UInt16 i = (UInt16)(x - 1); i <= (UInt16)(x + 1); i++)
            {
                for (UInt16 j = (UInt16)(y - 1); j <= (UInt16)(y + 1); j++)
                {
                    UInt16 p = (UInt16)(j * size + i);

                    if (i == x && j == y)
                    {
                        continue;
                    }

                    if (values[p] == 1)
                    {
                        count1++;
                    }
                }
            }

            return count1;
        }

        private Byte GetValue(Byte value, Byte neighboursCount)
        {
            if (value == 1)
            {
                if (neighboursCount == 2 || neighboursCount == 3)
                {
                    return 1;
                }
            }
            else if (value == 0)
            {
                if (neighboursCount == 3)
                {
                    return 0;
                }
            }

            return 0;
        }
    }
}
