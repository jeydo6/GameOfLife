using System;

namespace GameOfLife.Domain.Entities
{
    public class Field
    {
        private readonly Action<Field> _lifeAction;

        public Field(UInt16 size, Byte density, Action<Field> lifeAction)
        {
            Id = Guid.NewGuid();
            Size = (UInt16)(size + 2);
            Values = Generate(density, 0);

            _lifeAction = lifeAction;
        }

        public Guid Id { get; }

        public UInt32 Generation { get; private set; }

        public Boolean[,] Values { get; }

        public UInt16 Size { get; }

        public Field Next()
        {
            _lifeAction?.Invoke(this);

            Generation++;

            return this;
        }

        private Boolean[,] Generate(Byte density, Int32 seed)
        {
            Boolean[,] values = new Boolean[Size, Size];

            Random random = new Random(seed);
            for (UInt16 i = 1; i < Size - 1; i++)
            {
                for (UInt16 j = 1; j < Size - 1; j++)
                {
                    values[i, j] = random.Next(0, Byte.MaxValue - density) == 0;
                }
            }

            return values;
        }
    }
}
