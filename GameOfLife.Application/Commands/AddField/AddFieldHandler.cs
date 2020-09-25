using GameOfLife.Domain.Entities;
using GameOfLife.Domain.Repository;
using MediatR;
using System;
using System.Collections;
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
            Boolean[,] values = field.Values.Clone() as Boolean[,];

            for (UInt16 i = 1; i < size - 1; i++)
            {
                for (UInt16 j = 1; j < size - 1; j++)
                {
                    field.Values[i, j] = GetValue(
                        field.Values[i, j],
                        GetNeighboursCount(values, x: i, y: j)
                    );
                }
            }
        }

        private Byte GetNeighboursCount(Boolean[,] values, UInt16 x, UInt16 y)
        {
            Byte count = 0;

            for (UInt16 i = (UInt16)(x - 1); i <= (UInt16)(x + 1); i++)
            {
                for (UInt16 j = (UInt16)(y - 1); j <= (UInt16)(y + 1); j++)
                {
                    if (i == x && j == y)
                    {
                        continue;
                    }

                    if (values[i, j])
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private Boolean GetValue(Boolean value, Byte neighboursCount)
        {
            if (value)
            {
                if (neighboursCount == 2 || neighboursCount == 3)
                {
                    return true;
                }
            }
            else
            {
                if (neighboursCount == 3)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
