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
                new Field(request.Size, 253, Life)
            );
        }

        private void Life(Field field)
        {
            UInt16 size = field.Size;
            Boolean[,] values = field.Values.Clone() as Boolean[,];

            for (UInt16 i = 0; i < size; i++)
            {
                for (UInt16 j = 0; j < size; j++)
                {
                    field.Values[i, j] = GetValue(
                        field.Values[i, j],
                        GetNeighboursCount(values, size, x: i, y: j)
                    );
                }
            }
        }

        private Byte GetNeighboursCount(Boolean[,] values, UInt16 size, UInt16 x, UInt16 y)
        {
            Byte count = 0;

            for (UInt16 i = (UInt16)Math.Max(x - 1, 0); i <= (UInt16)Math.Min(x + 1, size - 1); i++)
            {
                for (UInt16 j = (UInt16)Math.Max(y - 1, 0); j <= (UInt16)Math.Min(y + 1, size - 1); j++)
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
